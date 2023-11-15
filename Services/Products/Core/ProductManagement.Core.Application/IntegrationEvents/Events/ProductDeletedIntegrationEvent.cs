using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace ProductManagement.Core.Application.IntegrationEvents.Events;

public record ProductDeletedIntegrationEvent(Guid ProductId, DateTime? DeletedAt, string? DeletedBy) : IntegrationEvent;