using TinyCRM.Core.IntegrationEvents.Events;

namespace TinyCRM.Core.IntegrationEvents.Handlers;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}
