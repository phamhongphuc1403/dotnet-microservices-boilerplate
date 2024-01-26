namespace BuildingBlock.Core.Application.Services.EmailService;

public class EmailConfiguration
{
    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string DisplayName { get; set; } = null!;
}