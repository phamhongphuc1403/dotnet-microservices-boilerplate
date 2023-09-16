using BuildingBlock.Core.IntegrationEvents.Events;

namespace BuildingBlock.Core.IntegrationEvents.Handlers;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}
