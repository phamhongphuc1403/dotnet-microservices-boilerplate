using BuildingBlock.Core.Domain;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

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