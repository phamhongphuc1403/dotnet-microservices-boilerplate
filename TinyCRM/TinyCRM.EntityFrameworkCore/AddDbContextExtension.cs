using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TinyCRM.EntityFrameworkCore.Data;

namespace TinyCRM.EntityFrameworkCore;

public static class AddDbContextExtension
{
    public static IServiceCollection AddAddDbContextExtension(this IServiceCollection services,
        string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}