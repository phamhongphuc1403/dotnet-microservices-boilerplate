using MediatR;
using Microsoft.Extensions.Logging;
using TinyCRM.Core.IntegrationEvents.Handlers;
using TinyCRM.SaleManagement.Application.IntegrationEvents.Events;

namespace TinyCRM.SaleManagement.Application.IntegrationEvents.Handlers;

public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductCreatedIntegrationEventHandler> _logger;

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
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            
        }

        return Task.CompletedTask;
    }
}