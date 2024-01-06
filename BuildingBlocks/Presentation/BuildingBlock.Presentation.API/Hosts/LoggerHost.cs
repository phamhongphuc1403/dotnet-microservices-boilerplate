using BuildingBlock.Infrastructure.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Presentation.API.Hosts;

public static class LoggerHost
{
    public static IHostBuilder UseLogger(this IHostBuilder hosts, IConfiguration configuration)
    {
        hosts.UseLoggerService(configuration);

        return hosts;
    }
}