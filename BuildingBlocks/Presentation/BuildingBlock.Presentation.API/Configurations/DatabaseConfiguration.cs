namespace BuildingBlock.Presentation.API.Configurations;

public class DatabaseConfiguration
{
    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public string Database { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public override string ToString()
    {
        return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
}