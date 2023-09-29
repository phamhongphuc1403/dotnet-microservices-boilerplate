using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identities.EntityFrameworkCore.Entities;

public class ApplicationRole : IdentityRole
{
    public ICollection<IdentityRoleClaim<string>> Claims { get; set; } = null!;
}