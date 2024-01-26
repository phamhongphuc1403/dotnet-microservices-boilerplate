using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Specifications;

public class DeviceTokenTokenSpecification : Specification<DeviceToken>
{
    private readonly string _token;

    public DeviceTokenTokenSpecification(string token)
    {
        _token = token;
    }

    public override Expression<Func<DeviceToken, bool>> ToExpression()
    {
        return token => token.Token == _token;
    }
}