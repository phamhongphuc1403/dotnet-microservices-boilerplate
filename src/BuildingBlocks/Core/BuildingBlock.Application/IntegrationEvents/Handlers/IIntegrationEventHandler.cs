using BuildingBlock.Application.IntegrationEvents.Events;

namespace BuildingBlock.Application.IntegrationEvents.Handlers;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}