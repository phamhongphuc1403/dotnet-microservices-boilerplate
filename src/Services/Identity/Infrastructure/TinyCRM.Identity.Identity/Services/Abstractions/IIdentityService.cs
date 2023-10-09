using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}