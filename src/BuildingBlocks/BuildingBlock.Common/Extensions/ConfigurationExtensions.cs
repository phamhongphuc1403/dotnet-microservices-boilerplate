using Microsoft.Extensions.Configuration;

namespace BuildingBlock.Common.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string name)
    {
        return configuration[name] ?? throw new InvalidOperationException(
            $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}");
    }

    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ?? throw new InvalidOperationException(
            $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":ConnectionStrings:" + name : "ConnectionStrings:" + name)}");
    }

    public static T BindAndGetConfig<T>(this IConfiguration configuration, string sectionName)
    {
        var config = configuration.GetSection(sectionName).Get<T>();
        configuration.Bind(config);
        if (config == null) throw new Exception($"{sectionName} configuration is not provided.");

        return config;
    }
}