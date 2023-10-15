using TinyCRM.Identity.Identity.RoleAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.Identity.Common.Services.Abstractions;

public interface IIdentityService
{
    Task<ApplicationUser> GetApplicationUserByIdAsync(Guid userId);
    Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName);
}