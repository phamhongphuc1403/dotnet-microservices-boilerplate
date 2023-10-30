using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

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