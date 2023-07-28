using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public string? Name { get; set; }
        public string? RefreshToken { get; set; }
    }
}