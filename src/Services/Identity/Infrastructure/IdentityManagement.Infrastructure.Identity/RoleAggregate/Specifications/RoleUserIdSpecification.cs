using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

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