using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.PermissionAggregate.Specifications;

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