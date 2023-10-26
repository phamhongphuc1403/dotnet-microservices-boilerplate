using System.Security.Claims;
using BuildingBlock.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace Identity.API.Authorization;

public class IdentityPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public IdentityPermissionAuthorizationHandler(IUserReadOnlyRepository userReadOnlyRepository,
        IRoleReadOnlyRepository roleReadOnlyRepository, IPermissionReadOnlyRepository permissionReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return;

        var user = await _userReadOnlyRepository.GetByIdAsync(new Guid(userId));

        if (user == null) return;

        var roles = await _roleReadOnlyRepository.GetNameByUserIdAsync(user.Id);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(role);

            permissions.AddRange(rolePermissions);
        }

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}