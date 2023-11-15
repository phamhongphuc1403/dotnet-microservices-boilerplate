using BuildingBlock.Core.Application.IntegrationEvents.Events;
using BuildingBlock.Core.Application.IntegrationEvents.Handlers;

namespace BuildingBlock.Core.Application.EventBus.Abstractions;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }

    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

    bool HasSubscriptionsForEvent(string eventName);

    Type? GetEventTypeByName(string eventName);

    IEnumerable<Type> GetHandlersForEvent<T>() where T : IntegrationEvent;

    IEnumerable<Type> GetHandlersForEvent(string eventName);

    string GetEventKey<T>();
}