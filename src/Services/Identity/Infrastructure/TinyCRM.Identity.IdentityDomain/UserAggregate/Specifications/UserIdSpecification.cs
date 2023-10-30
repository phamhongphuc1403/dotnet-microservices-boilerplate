using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.UserAggregate.Specifications;

public class UserIdSpecification : Specification<ApplicationUser>
{
    private readonly Guid _id;

    public UserIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return user => user.Id == _id;
    }
}