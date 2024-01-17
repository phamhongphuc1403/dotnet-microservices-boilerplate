using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Specifications;

public class RoleNameExactMatchSpecification : Specification<Role>
{
    private readonly string _name;

    public RoleNameExactMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<Role, bool>> ToExpression()
    {
        return role => role.Name.ToUpper() == _name.ToUpper();
    }
}