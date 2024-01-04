using BuildingBlock.Core.Application.IntegrationEvents.Events;

namespace IdentityManagement.Core.Application.Users.IntegrationEvents.Events;

public record UserCreatedIntegrationEvent(Guid UserId, string Name, string? AvatarUrl, string? CoverUrl,
    DateTime CreatedAt, string CreatedBy) : EntityCreatedIntegrationEvent(CreatedAt, CreatedBy);