using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Application.EventBus.Implementations;
using BuildingBlock.Infrastructure.RabbitMQ;
using BuildingBlock.Presentation.API.Configurations;
using BuildingBlock.Presentation.API.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BuildingBlock.Presentation.API.Extensions;

public static class EventBusExtension
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "EventBus": {
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1,
        //     "HostName": "...",
        //     "Port": "..."
        //   }
        // }

        var eventBusConfiguration = configuration.BindAndGetConfig<EventBusConfiguration>("EventBus");

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory
            {
                HostName = eventBusConfiguration.HostName,
                Port = eventBusConfiguration.Port,
                UserName = eventBusConfiguration.UserName,
                Password = eventBusConfiguration.Password,
                DispatchConsumersAsync = true
            };

            return new RabbitMQPersistentConnection(factory, logger, eventBusConfiguration.RetryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        {
            var subscriptionClientName = eventBusConfiguration.SubscriptionClientName;
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var retryCount = eventBusConfiguration.RetryCount;

            return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager,
                subscriptionClientName, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

        return services;
    }
}