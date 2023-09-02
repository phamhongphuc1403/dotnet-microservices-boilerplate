using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TinyCRM.ProductManagement.EntityFrameworkCore.Extensions;

public static class AddDbContext
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ProductDbContext>(options =>
        {
            // options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}