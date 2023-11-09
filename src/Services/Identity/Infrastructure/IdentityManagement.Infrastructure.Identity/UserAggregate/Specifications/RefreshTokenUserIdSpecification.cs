using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;

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