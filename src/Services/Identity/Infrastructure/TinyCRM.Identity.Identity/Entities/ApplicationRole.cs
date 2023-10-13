using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.Entities;

public sealed class ApplicationRole : IdentityRole
{
    public ApplicationRole(string roleName) : this()
    {
        Name = roleName;
    }

    public ApplicationRole()
    {
    }

    public ICollection<IdentityRoleClaim<string>> Claims { get; set; } = null!;
}