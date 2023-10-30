using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.PermissionAggregate.Specifications;

public class PermissionRoleNameSpecification : Specification<ApplicationPermission>
{
    private readonly string _roleName;

    public PermissionRoleNameSpecification(string roleName)
    {
        _roleName = roleName;
    }

    public override Expression<Func<ApplicationPermission, bool>> ToExpression()
    {
        return permission => permission.Role.NormalizedName == _roleName.ToUpper();
    }
}