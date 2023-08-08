using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Infrastructure.Identity.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; } = new HashSet<IdentityRoleClaim<string>>();
    }
}
