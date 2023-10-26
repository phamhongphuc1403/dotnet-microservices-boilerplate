using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.UserAggregate.Specifications;

public class RefreshTokenUserIdSpecification : Specification<ApplicationRefreshToken>
{
    private readonly Guid _userId;

    public RefreshTokenUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<ApplicationRefreshToken, bool>> ToExpression()
    {
        return refreshToken => refreshToken.UserId == _userId;
    }
}