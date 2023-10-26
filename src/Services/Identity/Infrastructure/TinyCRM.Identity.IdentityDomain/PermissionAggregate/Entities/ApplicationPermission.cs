using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

public class ApplicationPermission : IdentityRoleClaim<Guid>, IAggregateRoot
{
    public ApplicationRole Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public new Guid Id { get; set; }
}