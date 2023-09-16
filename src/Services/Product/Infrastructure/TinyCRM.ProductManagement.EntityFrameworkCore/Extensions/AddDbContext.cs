using BuildingBlock.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TinyCRM.ProductManagement.EntityFrameworkCore.Extensions;

public static class AddDbContextExtension
{
    public static IServiceCollection AddDbContext<T>(this IServiceCollection services) where T : BaseAppDbContext
    {
        services.AddDbContext<T>(options =>
        {
            // options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}