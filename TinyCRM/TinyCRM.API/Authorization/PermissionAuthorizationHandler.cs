using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.API.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IIdentityRoleService _identityRoleService;

    public PermissionAuthorizationHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var role = Optional<Claim>.Of(context.User.FindFirst(ClaimTypes.Role))
            .ThrowIfNotPresent(new UnauthorizedException()).Get();

        var claims = await _identityRoleService.GetAllPermissionsByRoleName(role.Value);

        if (claims.Any(claim => claim.Type == requirement.Permission))
            context.Succeed(requirement);
        else
            throw new ForbiddenException();
    }
}