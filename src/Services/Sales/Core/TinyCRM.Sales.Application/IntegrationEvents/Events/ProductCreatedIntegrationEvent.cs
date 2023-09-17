using BuildingBlock.Application.IntegrationEvents.Events;

namespace TinyCRM.Sales.Application.IntegrationEvents.Events;

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