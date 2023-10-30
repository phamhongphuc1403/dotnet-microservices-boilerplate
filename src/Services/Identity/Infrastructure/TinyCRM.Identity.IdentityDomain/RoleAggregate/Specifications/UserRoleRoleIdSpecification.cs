using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

public class UserRoleRoleIdSpecification : Specification<ApplicationUserRole>
{
    private readonly Guid _roleId;

    public UserRoleRoleIdSpecification(Guid roleId)
    {
        _roleId = roleId;
    }

    public override Expression<Func<ApplicationUserRole, bool>> ToExpression()
    {
        return userRole => userRole.RoleId == _roleId;
    }
}