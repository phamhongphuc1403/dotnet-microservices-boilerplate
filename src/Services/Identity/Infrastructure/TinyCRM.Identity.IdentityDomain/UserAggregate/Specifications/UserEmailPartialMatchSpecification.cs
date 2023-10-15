using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.Identity.UserAggregate.Specifications;

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

        return user => user.Email!.ToUpper().Contains(_email.ToUpper());
    }
}