using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.PermissionAggregate.Specifications;

public class PermissionRoleIdSpecification : Specification<ApplicationPermission>
{
    private readonly Guid _roleId;

    public PermissionRoleIdSpecification(Guid roleId)
    {
        _roleId = roleId;
    }

    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return permission => permission.RoleId == _roleId;
    }
}