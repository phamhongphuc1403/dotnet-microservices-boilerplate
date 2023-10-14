using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.Entities;

public sealed class ApplicationRole : IdentityRole
{
    public ApplicationRole(string roleName, string? email) : this(roleName)
    {
        if (email != null) CreatedBy = email;
    }

    public ApplicationRole(string roleName) : this()
    {
        CreatedBy = "admin";
        CreatedAt = DateTime.UtcNow;
        Name = roleName;
    }

    public ApplicationRole()
    {
    }

    public ICollection<IdentityRoleClaim<string>> Claims { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}