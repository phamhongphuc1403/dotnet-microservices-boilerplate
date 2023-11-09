using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlock.Presentation.API.Extensions;

public static class CacheExtension
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetRequiredConnectionString("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}