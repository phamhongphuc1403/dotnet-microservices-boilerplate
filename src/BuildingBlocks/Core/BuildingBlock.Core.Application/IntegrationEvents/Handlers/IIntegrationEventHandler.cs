using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace BuildingBlock.Core.Application.IntegrationEvents.Handlers;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent @event);
}