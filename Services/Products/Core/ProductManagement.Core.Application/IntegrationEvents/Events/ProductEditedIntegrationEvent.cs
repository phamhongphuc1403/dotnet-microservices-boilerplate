using BuildingBlock.Core.Application.IntegrationEvents.Events;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Application.IntegrationEvents.Events;

public record ProductEditedIntegrationEvent(Guid ProductId, string ProductCode, string ProductName,
    double ProductPrice, bool ProductIsAvailable, ProductType ProductType, DateTime? UpdatedAt,
    string? UpdatedBy) : IntegrationEvent;