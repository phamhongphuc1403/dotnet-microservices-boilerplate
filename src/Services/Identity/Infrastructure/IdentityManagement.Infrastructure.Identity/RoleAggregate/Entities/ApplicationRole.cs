using BuildingBlock.Core.Domain;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

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