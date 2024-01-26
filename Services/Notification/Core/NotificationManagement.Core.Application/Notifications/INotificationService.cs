namespace NotificationManagement.Core.Application.Notifications;

public interface INotificationService
{
    public Task SendAsync(Guid userId, NotificationMessage notification);

    public Task SendAsync(IEnumerable<Guid> userIds, NotificationMessage notification);
}