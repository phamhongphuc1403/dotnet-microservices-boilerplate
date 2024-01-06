namespace BuildingBlock.Presentation.API.Configurations;

public class EventBusConfiguration
{
    public string HostName { get; set; } = null!;

    public string SubscriptionClientName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RetryCount { get; set; } = 5;


    public int Port { get; set; }
}