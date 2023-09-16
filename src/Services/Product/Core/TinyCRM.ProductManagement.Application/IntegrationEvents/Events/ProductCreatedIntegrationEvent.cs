using BuildingBlock.Core.IntegrationEvents.Events;

namespace TinyCRM.ProductManagement.Application.IntegrationEvents.Events;


public record ProductCreatedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; }
    public Guid ProductCode { get; }

    public ProductCreatedIntegrationEvent(Guid productId, Guid productCode)
    {
        ProductId = productId;
        ProductCode = productCode;
    }
}