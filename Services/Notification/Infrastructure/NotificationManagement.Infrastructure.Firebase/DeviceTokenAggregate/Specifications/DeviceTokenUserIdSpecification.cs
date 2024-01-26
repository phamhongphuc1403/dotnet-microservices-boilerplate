using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Specifications;

public class DeviceTokenUserIdSpecification : Specification<DeviceToken>
{
    private readonly Guid _userId;

    public DeviceTokenUserIdSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<DeviceToken, bool>> ToExpression()
    {
        return deviceToken => deviceToken.UserId == _userId;
    }
}