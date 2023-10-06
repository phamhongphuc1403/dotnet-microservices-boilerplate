using TinyCRM.Identity.Indentity.Entities;

namespace TinyCRM.Identity.Indentity.Services.Abstractions;

public interface IIdentityService
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}