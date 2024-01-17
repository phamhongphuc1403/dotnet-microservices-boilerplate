using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class UserRefreshTokenSpecification : Specification<User>
{
    private readonly string _refreshToken;

    public UserRefreshTokenSpecification(string refreshToken)
    {
        _refreshToken = refreshToken;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.RefreshTokens.Any(rf => rf.Token == _refreshToken);
    }
}