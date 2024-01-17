using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class UserNamePartialMatchSpecification : Specification<User>
{
    private readonly string _name;

    public UserNamePartialMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Name.ToUpper().Contains(_name.ToUpper());
    }
}