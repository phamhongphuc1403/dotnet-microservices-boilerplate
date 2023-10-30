using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.IdentityDomain.UserAggregate.Specifications;

public class RefreshTokenRevokedAtIsNullSpecification : Specification<ApplicationRefreshToken>
{
    public override Expression<Func<ApplicationRefreshToken, bool>> ToExpression()
    {
        return refreshToken => refreshToken.RevokedAt == null;
    }
}