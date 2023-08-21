using Microsoft.AspNetCore.Identity;

namespace TinyCRM.EntityFrameworkCore.Identity.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? RefreshToken { get; set; }
}