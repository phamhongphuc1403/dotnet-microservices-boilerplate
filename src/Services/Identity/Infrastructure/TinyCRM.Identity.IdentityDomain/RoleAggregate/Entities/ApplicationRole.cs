using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

public sealed class ApplicationRole : IdentityRole<Guid>, IAggregateRoot
{
    public ICollection<ApplicationPermission> Permissions { get; set; } = null!;

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}