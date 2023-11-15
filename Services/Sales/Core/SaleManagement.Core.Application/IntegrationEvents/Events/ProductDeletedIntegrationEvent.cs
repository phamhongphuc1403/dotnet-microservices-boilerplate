using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace SaleManagement.Core.Application.IntegrationEvents.Events;

public record ProductDeletedIntegrationEvent(Guid ProductId, DateTime? DeletedAt, string? DeletedBy) : IntegrationEvent;