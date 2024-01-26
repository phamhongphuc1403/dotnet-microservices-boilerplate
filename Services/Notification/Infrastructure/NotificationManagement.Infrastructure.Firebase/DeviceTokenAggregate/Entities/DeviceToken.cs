using BuildingBlock.Core.Domain;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

public class DeviceToken : AggregateRoot
{
    public DeviceToken(string token, Guid userId)
    {
        Token = token;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public string Token { get; set; }
}