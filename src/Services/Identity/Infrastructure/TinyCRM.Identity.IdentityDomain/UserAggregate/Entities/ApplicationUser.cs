using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public ICollection<ApplicationRefreshToken> RefreshTokens { get; set; } = null!;

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}