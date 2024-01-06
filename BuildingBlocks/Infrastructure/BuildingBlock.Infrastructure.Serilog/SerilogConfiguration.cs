using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BuildingBlock.Infrastructure.Serilog;

public static class SerilogConfiguration
{
    public static IHostBuilder UseLoggerService(this IHostBuilder hosts, IConfiguration configuration)
    {
        hosts.UseSerilog((_, loggerConfig) => { loggerConfig.ReadFrom.Configuration(configuration); });

        return hosts;
    }
}