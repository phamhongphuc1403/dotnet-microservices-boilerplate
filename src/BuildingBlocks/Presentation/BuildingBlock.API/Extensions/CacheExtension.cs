using BuildingBlock.Application;
using BuildingBlock.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlock.API.Extensions;

public static class CacheExtension
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetRequiredConnectionString("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<IRoleCacheService, RoleCacheService>();
        services.AddScoped<IPermissionCacheService, PermissionCacheService>();

        return services;
    }
}