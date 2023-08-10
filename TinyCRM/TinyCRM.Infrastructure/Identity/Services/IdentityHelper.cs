using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Identity.Entities;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;

namespace TinyCRM.Infrastructure.Identity.Services;

public class IdentityHelper : IIdentityHelper
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityHelper(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
    {
        return Optional<ApplicationUser>.Of(await _userManager.FindByIdAsync(userId))
            .ThrowIfNotPresent(new NotFoundException("User not found")).Get();
    }

    public async Task<ApplicationRole> GetApplicationRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.Roles
            .Include(role => role.Claims)
            .FirstOrDefaultAsync(role => role.Name == roleName);

        return Optional<ApplicationRole>
            .Of(role)
            .ThrowIfNotPresent(new NotFoundException("Role not found")).Get();
    }
}