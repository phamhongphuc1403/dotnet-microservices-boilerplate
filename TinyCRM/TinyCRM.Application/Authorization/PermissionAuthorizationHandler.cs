using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TinyCRM.Application.Common.Interfaces;

namespace TinyCRM.Application.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IIdentityRoleService _identityRoleService;

        public PermissionAuthorizationHandler(IIdentityRoleService identityRoleService)
        {
            _identityRoleService = identityRoleService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var role = context.User.FindFirst(ClaimTypes.Role)!;
            var claims = await _identityRoleService.GetClaimsByRoleIdAsync(role.Value);

            if (claims.Any(claim => claim.Type == requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
