using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Exceptions;

public class RoleUserIdSpecification : Specification<Role>
{
    private readonly Guid _userId;

    public RoleUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<Role, bool>> ToExpression()
    {
        return role => role.UserRoles.Any(userRole => userRole.UserId == _userId);
    }
}