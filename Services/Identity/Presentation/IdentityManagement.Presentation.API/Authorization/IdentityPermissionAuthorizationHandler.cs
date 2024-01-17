using System.Security.Claims;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using BuildingBlock.Presentation.API.Authorization;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace IdentityManagement.Presentation.API.Authorization;

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

        var userIdSpecification = new EntityIdSpecification<User>(new Guid(userId));

        var isUserExist = await _userReadOnlyRepository.CheckIfExistAsync(userIdSpecification);

        if (isUserExist is false) return;

        var roles = await _roleReadOnlyRepository.GetNameByUserIdAsync(new Guid(userId));

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(role);

            permissions.AddRange(rolePermissions);
        }

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}