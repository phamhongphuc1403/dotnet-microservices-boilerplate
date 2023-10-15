using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identities.Domain.RoleAggregate.Exceptions;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Identity.Common.Services.Abstractions;
using TinyCRM.Identity.Identity.RoleAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.Identity.Common.Services.Implementations;

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

    public async Task<ApplicationUser> GetApplicationUserByIdAsync(Guid userId)
    {
        var user = await _userManager.Users.Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.Id == userId);

        return Optional<ApplicationUser>.Of(user)
            .ThrowIfNotPresent(new UserNotFoundException(userId)).Get();
    }

    public async Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.Roles.Include(role => role.Claims)
            .FirstOrDefaultAsync(role => role.Name == roleName);

        return Optional<ApplicationRole>.Of(role).ThrowIfNotPresent(new RoleNotFoundException(roleName))
            .Get();
    }
}