using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class UserRoleNameSpecification : Specification<User>
{
    private readonly string _roleName;

    public UserRoleNameSpecification(string roleName)
    {
        _roleName = roleName;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.UserRoles.Any(userRole => userRole.Role.Name.ToUpper() == _roleName.ToUpper());
    }
}