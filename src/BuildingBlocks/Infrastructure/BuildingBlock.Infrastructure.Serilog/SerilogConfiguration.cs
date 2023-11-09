using Microsoft.Extensions.Configuration;
using Serilog;

namespace BuildingBlock.Infrastructure.Serilog;

public static class SerilogConfiguration
{
    public static LoggerConfiguration CreateLogger(this LoggerConfiguration loggerConfig, IConfiguration configuration,
        string seqServerUrl, string serviceName)
    {
        loggerConfig
            .Enrich.WithProperty("ServiceName", serviceName)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .ReadFrom.Configuration(configuration);

        return loggerConfig;
    }
}