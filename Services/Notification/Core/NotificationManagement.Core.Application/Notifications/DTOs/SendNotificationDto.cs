namespace NotificationManagement.Core.Application.Notifications.DTOs;

public class SendNotificationDto
{
    public NotificationMessage Message { get; set; } = null!;

    public IEnumerable<Guid> UserIds { get; set; } = null!;
}