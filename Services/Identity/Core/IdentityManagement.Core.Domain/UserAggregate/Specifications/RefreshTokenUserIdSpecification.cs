using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class RefreshTokenUserIdSpecification : Specification<RefreshToken>
{
    private readonly Guid _userId;

    public RefreshTokenUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<RefreshToken, bool>> ToExpression()
    {
        return rf => rf.UserId == _userId;
    }
}