using BuildingBlock.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services,
        IConfiguration configuration) where TDbContext : BaseDbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                              configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        });
        return services;
    }

    public static async Task ApplyMigrationAsync<TDbContext>(this IServiceCollection services)
        where TDbContext : BaseDbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
    }
}