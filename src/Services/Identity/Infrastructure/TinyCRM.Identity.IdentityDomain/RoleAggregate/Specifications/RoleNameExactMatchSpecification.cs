using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

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