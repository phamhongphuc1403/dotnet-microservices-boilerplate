using BuildingBlock.Core.IntegrationEvents.Events;
using BuildingBlock.Core.IntegrationEvents.Handlers;

namespace BuildingBlock.Core.EventBus.Interfaces;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, TH>() 
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}