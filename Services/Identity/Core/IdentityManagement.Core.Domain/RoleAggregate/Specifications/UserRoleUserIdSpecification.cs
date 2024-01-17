using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Specifications;

public class UserRoleUserIdSpecification : Specification<UserRole>
{
    private readonly Guid _userId;

    public UserRoleUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<UserRole, bool>> ToExpression()
    {
        return userRole => userRole.UserId == _userId;
    }
}