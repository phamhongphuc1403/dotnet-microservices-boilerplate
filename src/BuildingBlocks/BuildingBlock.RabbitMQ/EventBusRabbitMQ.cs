using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Application.IntegrationEvents.Events;
using BuildingBlock.Application.IntegrationEvents.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace BuildingBlock.RabbitMQ;

public class EventBusRabbitMQ : IEventBus
{
    private const string BrokerName = "tinycrm_event_bus";

    private static readonly JsonSerializerOptions SIndentedOptions = new() { WriteIndented = true };

    private static readonly JsonSerializerOptions SCaseInsensitiveOptions =
        new() { PropertyNameCaseInsensitive = true };

    private readonly ILogger<EventBusRabbitMQ> _logger;

    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly string _queueName;
    private readonly int _retryCount;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBusSubscriptionsManager _subsManager;

    private IModel _consumerChannel;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
        IServiceProvider serviceProvider, IEventBusSubscriptionsManager subsManager, string queueName,
        int retryCount = 5)
    {
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _subsManager = subsManager;
        _queueName = queueName;
        _consumerChannel = CreateConsumerChannel();
        _serviceProvider = serviceProvider;
        _retryCount = retryCount;
    }

    public void Publish(IntegrationEvent @event)
    {
        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s", @event.Id,
                        $"{time.TotalSeconds:n1}");
                });

        var eventName = @event.GetType().Name;

        _logger.LogInformation("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id,
            eventName);

        var channel = _persistentConnection.CreateModel();

        _logger.LogInformation("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(BrokerName, "direct");

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), SIndentedOptions);

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            _logger.LogInformation("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(
                BrokerName,
                eventName,
                true,
                properties,
                body);
        });
    }

    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();
        DoInternalSubscription(eventName);

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH));

        _subsManager.AddSubscription<T, TH>();

        StartBasicConsume();
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _persistentConnection.CreateModel();

        channel.ExchangeDeclare(BrokerName,
            "direct");

        channel.QueueDeclare(_queueName,
            true,
            false,
            false,
            null);

        channel.CallbackException += (_, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

            _consumerChannel.QueueBind(_queueName,
                BrokerName,
                eventName);
        }
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

        consumer.Received += Consumer_Received;

        _consumerChannel.BasicConsume(
            _queueName,
            false,
            consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);
        }

        _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogInformation("Processing RabbitMQ event: {EventName}", eventName);

        if (_subsManager.HasSubscriptionsForEvent(eventName))
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var subscriptions = _subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                var handler = scope.ServiceProvider.GetService(subscription);

                if (handler == null) continue;

                var eventType = _subsManager.GetEventTypeByName(eventName) ??
                                throw new ArgumentNullException(eventName);

                var integrationEvent = JsonSerializer.Deserialize(message, eventType, SCaseInsensitiveOptions);

                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                await Task.Yield();

                await (Task)concreteType.GetMethod("Handle")?.Invoke(handler, new[] { integrationEvent })!;
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }
}