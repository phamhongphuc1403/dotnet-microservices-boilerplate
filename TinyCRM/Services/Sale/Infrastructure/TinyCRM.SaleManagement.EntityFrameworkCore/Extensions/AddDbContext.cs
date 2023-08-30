using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Extensions;

public static class AddDbContext
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<SaleDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}