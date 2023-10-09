using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<ApplicationRefreshToken> RefreshTokens { get; set; } = null!;
}