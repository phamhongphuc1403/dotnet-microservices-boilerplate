using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

public class RoleUserIdSpecification : Specification<ApplicationRole>
{
    private readonly Guid _userId;

    public RoleUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<ApplicationRole, bool>> ToExpression()
    {
        return role => role.UserRoles.Any(userRole => userRole.UserId == _userId);
    }
}