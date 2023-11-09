using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.API.Hosts;

public static class DefaultHosts
{
    public static IHostBuilder UseDefaultHosts(this IHostBuilder hosts, IConfiguration configuration)
    {
        hosts.UseLogger(configuration);
        
        return hosts;
    }
}