using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace IdentityManagement.Core.Application.Users.IntegrationEvents.Events;

public record UserDeletedIntegrationEvent(Guid UserId, DateTime? DeletedAt, string? DeletedBy) : IntegrationEvent;