using BuildingBlock.Core.Application.IntegrationEvents.Handlers;
using MediatR;
using Microsoft.Extensions.Logging;
using SaleManagement.Core.Application.IntegrationEvents.Events;

namespace SaleManagement.Core.Application.IntegrationEvents.Handlers;

public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    private readonly ILogger<ProductCreatedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public ProductCreatedIntegrationEventHandler(
        IMediator mediator,
        ILogger<ProductCreatedIntegrationEventHandler> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Handle(ProductCreatedIntegrationEvent @event)
    {
        using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", @event.Id) }))
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})",
                @event.Id, @event);
        }

        return Task.CompletedTask;
    }
}