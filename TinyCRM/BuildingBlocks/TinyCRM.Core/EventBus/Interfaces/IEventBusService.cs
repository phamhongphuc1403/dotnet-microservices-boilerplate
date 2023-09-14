using TinyCRM.Core.IntegrationEvents.Events;
using TinyCRM.Core.IntegrationEvents.Handlers;

namespace TinyCRM.Core.EventBus.Interfaces;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, TH>() 
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}