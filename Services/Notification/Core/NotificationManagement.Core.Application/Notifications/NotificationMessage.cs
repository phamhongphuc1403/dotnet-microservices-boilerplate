namespace NotificationManagement.Core.Application.Notifications;

public class NotificationMessage
{
    public NotificationMessage()
    {
    }

    public NotificationMessage(string tile, string body)
    {
        Title = tile;
        Body = body;
    }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;
}