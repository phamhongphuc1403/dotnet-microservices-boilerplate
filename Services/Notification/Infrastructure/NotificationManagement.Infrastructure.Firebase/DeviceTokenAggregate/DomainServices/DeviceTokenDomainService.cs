using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.DomainServices;

public class DeviceTokenDomainService : IDeviceTokenDomainService
{
    public DeviceToken Create(string token, Guid userId)
    {
        return new DeviceToken(token, userId);
    }

    public void Update(DeviceToken deviceToken)
    {
        deviceToken.ResetUpdatedTimeStamp();
    }
}