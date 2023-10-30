using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.UserAggregate.Specifications;

public class UserEmailExactMatchSpecification : Specification<ApplicationUser>
{
    private readonly string _email;

    public UserEmailExactMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_email)) return user => true;

        return user => user.NormalizedEmail! == _email.ToUpper();
    }
}