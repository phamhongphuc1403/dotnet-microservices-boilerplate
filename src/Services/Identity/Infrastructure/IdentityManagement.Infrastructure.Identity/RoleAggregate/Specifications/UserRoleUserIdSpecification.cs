using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

public class UserRoleUserIdSpecification : Specification<ApplicationUserRole>
{
    private readonly Guid _userId;

    public UserRoleUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<ApplicationUserRole, bool>> ToExpression()
    {
        return userRole => userRole.UserId == _userId;
    }
}