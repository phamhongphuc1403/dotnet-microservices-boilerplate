using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class RefreshTokenRevokedAtIsNullSpecification : Specification<RefreshToken>
{
    public override Expression<Func<RefreshToken, bool>> ToExpression()
    {
        return refreshToken => refreshToken.RevokedAt == null;
    }
}