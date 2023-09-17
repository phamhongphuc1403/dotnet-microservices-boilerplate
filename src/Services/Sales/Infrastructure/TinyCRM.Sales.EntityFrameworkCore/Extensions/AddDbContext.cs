using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TinyCRM.Sales.EntityFrameworkCore.Extensions;

public static class AddDbContext
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<SaleDbContext>(options =>
        {
            options.UseNpgsql(
                // "Host=localhost; Port=5432; Database=TinyCRM.Sale; Username=postgres; Password=password"
                Environment.GetEnvironmentVariable("CONNECTION_STRING")
            );
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}