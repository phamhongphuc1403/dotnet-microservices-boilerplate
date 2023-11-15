using BuildingBlock.Infrastructure.Serilog;
using BuildingBlock.Presentation.API.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BuildingBlock.Presentation.API.Hosts;

public static class LoggerHost
{
    public static IHostBuilder UseLogger(this IHostBuilder hosts, IConfiguration configuration)
    {
        var seqServerUrl = configuration.GetRequiredValue("Serilog:SeqServerUrl");
        var serviceName = configuration.GetRequiredValue("Serilog:ServiceName");

        hosts.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.CreateLogger(configuration, seqServerUrl, serviceName);
        });

        return hosts;
    }
}