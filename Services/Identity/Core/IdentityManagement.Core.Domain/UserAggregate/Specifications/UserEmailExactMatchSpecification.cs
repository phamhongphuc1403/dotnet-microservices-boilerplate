using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class UserEmailExactMatchSpecification : Specification<User>
{
    private readonly string _email;

    public UserEmailExactMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email.ToUpper() == _email.ToUpper();
    }
}