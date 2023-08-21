using TinyCRM.Identity.Entities;

namespace TinyCRM.Identity.Services.Interfaces;

public interface IIdentityHelper
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);

    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}