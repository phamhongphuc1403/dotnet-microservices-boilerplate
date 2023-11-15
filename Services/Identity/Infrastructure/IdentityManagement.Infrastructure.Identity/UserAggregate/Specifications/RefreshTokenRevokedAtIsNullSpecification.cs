using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;

public class RefreshTokenRevokedAtIsNullSpecification : Specification<ApplicationRefreshToken>
{
    public override Expression<Func<ApplicationRefreshToken, bool>> ToExpression()
    {
        return refreshToken => refreshToken.RevokedAt == null;
    }
}