using TinyCRM.Core.IntegrationEvents.Events;
using TinyCRM.Core.IntegrationEvents.Handlers;

namespace TinyCRM.Core.EventBus.Interfaces;

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