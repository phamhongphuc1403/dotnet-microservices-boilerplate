using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.Entities;

public class ApplicationRole : IdentityRole
{
    public ICollection<IdentityRoleClaim<string>> Claims { get; set; } = null!;
}