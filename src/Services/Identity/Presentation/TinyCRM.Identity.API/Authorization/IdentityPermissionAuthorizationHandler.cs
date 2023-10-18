using System.Security.Claims;
using BuildingBlock.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;

namespace Identities.API.Authorization;

public class IdentityPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionDomainService _permissionDomainService;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IUserDomainService _userDomainService;

    public IdentityPermissionAuthorizationHandler(IPermissionDomainService permissionDomainService,
        IRoleDomainService roleDomainService,
        IUserDomainService userDomainService)
    {
        _permissionDomainService = permissionDomainService;
        _roleDomainService = roleDomainService;
        _userDomainService = userDomainService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return;

        var user = await _userDomainService.GetByIdAsync(new Guid(userId));

        if (user == null) return;

        var roles = await _roleDomainService.GetManyAsync(user);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionDomainService.GetPermissionsAsync(role);

            permissions.AddRange(rolePermissions);
        }

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}