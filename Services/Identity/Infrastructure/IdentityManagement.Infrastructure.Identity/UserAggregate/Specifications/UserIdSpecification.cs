using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;

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