using BuildingBlock.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.IntegrationEvents.Events;

public record ProductEditedIntegrationEvent(Guid ProductId, string ProductCode, string ProductName,
    double ProductPrice, bool ProductIsAvailable, ProductType ProductType, DateTime? UpdatedAt,
    string? UpdatedBy) : IntegrationEvent;