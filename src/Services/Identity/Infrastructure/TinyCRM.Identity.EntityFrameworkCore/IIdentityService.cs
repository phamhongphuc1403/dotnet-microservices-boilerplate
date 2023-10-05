using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public interface IIdentityService
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}