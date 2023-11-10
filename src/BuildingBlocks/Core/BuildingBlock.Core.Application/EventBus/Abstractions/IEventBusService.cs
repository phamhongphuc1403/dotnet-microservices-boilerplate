using BuildingBlock.Core.Application.IntegrationEvents.Events;
using BuildingBlock.Core.Application.IntegrationEvents.Handlers;

namespace BuildingBlock.Core.Application.EventBus.Abstractions;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}