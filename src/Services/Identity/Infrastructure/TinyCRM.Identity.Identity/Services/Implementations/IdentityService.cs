using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identities.Domain.RoleAggregate.Exceptions;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Identity.Entities;
using TinyCRM.Identity.Identity.Services.Abstractions;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityService : IIdentityService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
    {
        return Optional<ApplicationUser>.Of(await _userManager.FindByIdAsync(userId))
            .ThrowIfNotPresent(new UserNotFoundException(userId)).Get();
    }

    public async Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.Roles.Include(role => role.Claims)
            .FirstOrDefaultAsync(role => role.Name == roleName);

        return Optional<ApplicationRole>.Of(role).ThrowIfNotPresent(new RoleNotFoundException("role name", roleName))
            .Get();
    }
}