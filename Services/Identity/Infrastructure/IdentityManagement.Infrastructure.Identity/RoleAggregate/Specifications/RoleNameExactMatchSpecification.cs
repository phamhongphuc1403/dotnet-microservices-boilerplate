using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

public class RoleNameExactMatchSpecification : Specification<ApplicationRole>
{
    private readonly string _roleName;

    public RoleNameExactMatchSpecification(string roleName)
    {
        _roleName = roleName;
    }

    public override Expression<Func<ApplicationRole, bool>> ToExpression()
    {
        return role => role.NormalizedName == _roleName.ToUpper();
    }
}