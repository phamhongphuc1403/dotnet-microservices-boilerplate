using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>, IEntity
{
    public ApplicationUser User { get; set; } = null!;

    public ApplicationRole Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public Guid Id { get; set; }
}