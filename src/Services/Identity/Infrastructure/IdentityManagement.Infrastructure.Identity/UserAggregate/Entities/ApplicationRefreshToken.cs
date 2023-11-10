using BuildingBlock.Core.Domain;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

public class ApplicationRefreshToken : IEntity
{
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime? RevokedAt { get; set; }

    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}