using BuildingBlock.Core.Application.IntegrationEvents.Events;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Application.IntegrationEvents.Events;

public record ProductCreatedIntegrationEvent(Guid ProductId, string ProductCode, string ProductName,
    double ProductPrice, bool ProductIsAvailable, ProductType ProductType, DateTime CreatedAt,
    string CreatedBy) : IntegrationEvent;