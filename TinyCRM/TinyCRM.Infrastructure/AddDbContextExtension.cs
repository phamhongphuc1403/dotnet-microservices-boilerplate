using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TinyCRM.Infrastructure.Data;

namespace TinyCRM.Infrastructure
{
    public static class AddDbContextExtension
    {
        public static IServiceCollection AddAddDbContextExtension(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}