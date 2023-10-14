using System.Security.Claims;
using BuildingBlock.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace Identities.API.Authorization;

public class IdentityPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public IdentityPermissionAuthorizationHandler(IPermissionService permissionService, IRoleService roleService,
        IUserService userService)
    {
        _permissionService = permissionService;
        _roleService = roleService;
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return;

        var user = await _userService.GetByIdAsync(new Guid(userId));

        if (user == null) return;

        var roles = await _roleService.GetManyAsync(user);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionService.GetPermissionsAsync(role);

            permissions.AddRange(rolePermissions);
        }

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}