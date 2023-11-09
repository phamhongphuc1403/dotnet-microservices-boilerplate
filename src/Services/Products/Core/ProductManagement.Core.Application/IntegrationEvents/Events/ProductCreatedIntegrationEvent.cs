using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace ProductManagement.Core.Application.IntegrationEvents.Events;

public record ProductCreatedIntegrationEvent : IntegrationEvent
{
    public ProductCreatedIntegrationEvent(Guid productId, Guid productCode)
    {
        ProductId = productId;
        ProductCode = productCode;
    }

    public Guid ProductId { get; }
    public Guid ProductCode { get; }
}