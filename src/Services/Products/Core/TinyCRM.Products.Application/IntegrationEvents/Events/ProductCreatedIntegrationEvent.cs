using BuildingBlock.Application.IntegrationEvents.Events;

namespace TinyCRM.Products.Application.IntegrationEvents.Events;


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