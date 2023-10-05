using BuildingBlock.Application;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class IdentityPermissionService : IPermissionService
{
    private readonly IIdentityService _identityService;
    private readonly IPermissionCacheService _permissionCacheService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentityPermissionService(IPermissionCacheService permissionCacheService,
        RoleManager<ApplicationRole> roleManager, IIdentityService identityService)
    {
        _permissionCacheService = permissionCacheService;
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task<IEnumerable<string>> GetPermissionsAsync(string roleName)
    {
        var cachedRolePermissions = await _permissionCacheService.GetAsync(roleName);

        if (cachedRolePermissions != null) return cachedRolePermissions;

        var role = await _identityService.GetApplicationRoleByNameAsync(roleName);

        var claims = await _roleManager.GetClaimsAsync(role);

        var permissions = claims.Select(claim => claim.Type).ToList();

        await _permissionCacheService.SetAsync(roleName, permissions);

        return permissions;
    }
}