using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;

public class UserEmailPartialMatchSpecification : Specification<ApplicationUser>
{
    private readonly string _email;

    public UserEmailPartialMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_email)) return user => true;

        return user => user.NormalizedEmail!.Contains(_email.ToUpper());
    }
}