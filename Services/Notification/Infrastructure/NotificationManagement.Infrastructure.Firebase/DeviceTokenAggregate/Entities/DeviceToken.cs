using BuildingBlock.Core.Domain;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

public class DeviceToken : AggregateRoot
{
    public DeviceToken(string token)
    {
        Token = token;
    }

    public Guid UserId { get; set; }

    public string Token { get; set; }
}