using TinyCRM.EntityFrameworkCore.Identity.Entities;

namespace TinyCRM.EntityFrameworkCore.Identity.Services.Interfaces;

public interface IIdentityHelper
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);

    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}