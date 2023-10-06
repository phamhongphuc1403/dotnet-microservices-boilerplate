using BuildingBlock.Application.IntegrationEvents.Events;
using BuildingBlock.Application.IntegrationEvents.Handlers;

namespace BuildingBlock.Application.EventBus.Abstractions;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}