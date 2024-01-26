using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;

namespace NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.DomainServices;

public interface IDeviceTokenDomainService
{
    DeviceToken Create(string token, Guid userId);

    void Update(DeviceToken deviceToken);
}