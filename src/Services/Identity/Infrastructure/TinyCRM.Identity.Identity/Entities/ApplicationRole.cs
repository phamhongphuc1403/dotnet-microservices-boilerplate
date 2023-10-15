using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.Entities;

public sealed class ApplicationRole : IdentityRole<Guid>, IAggregateRoot
{
    public ApplicationRole(string roleName, string? email) : this(roleName)
    {
        if (email != null) CreatedBy = email;
    }

    public ApplicationRole(string roleName) : this()
    {
        CreatedBy = "server";
        CreatedAt = DateTime.UtcNow;
        Name = roleName;
    }

    public ApplicationRole()
    {
    }

    public ICollection<IdentityRoleClaim<Guid>> Claims { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}