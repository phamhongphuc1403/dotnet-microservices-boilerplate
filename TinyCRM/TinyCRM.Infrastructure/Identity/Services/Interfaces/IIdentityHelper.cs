using TinyCRM.Infrastructure.Identity.Entities;

namespace TinyCRM.Infrastructure.Identity.Services.Interfaces;

public interface IIdentityHelper
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);

    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}