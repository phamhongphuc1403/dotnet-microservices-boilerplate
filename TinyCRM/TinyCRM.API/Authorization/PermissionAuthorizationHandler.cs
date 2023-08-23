using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TinyCRM.Application.Modules.Permission.Services;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.API.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionCacheService _permissionCacheService;


    public PermissionAuthorizationHandler(
        IPermissionCacheService permissionCacheService
    )
    {
        _permissionCacheService = permissionCacheService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var role = Optional<Claim>.Of(context.User.FindFirst(ClaimTypes.Role))
            .ThrowIfNotPresent(new UnauthorizedException()).Get();

        var permissions = await _permissionCacheService.GetAllOrAddByRoleName(role.Value);

        if (permissions.Any(permission => permission.Type == requirement.Permission))
            context.Succeed(requirement);
        else
            throw new ForbiddenException();
    }
}