using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Specifications;

public class UserRoleRoleIdSpecification : Specification<UserRole>
{
    private readonly Guid _roleId;

    public UserRoleRoleIdSpecification(Guid roleId)
    {
        _roleId = roleId;
    }

    public override Expression<Func<UserRole, bool>> ToExpression()
    {
        return userRole => userRole.RoleId == _roleId;
    }
}