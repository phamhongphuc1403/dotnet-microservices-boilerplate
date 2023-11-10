using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.PermissionAggregate.Specifications;

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