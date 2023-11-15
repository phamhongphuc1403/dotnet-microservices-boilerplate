using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

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