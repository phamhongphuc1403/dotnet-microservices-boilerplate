using Microsoft.EntityFrameworkCore;
using TinyCRM.ProductManagement.EntityFrameworkCore;

namespace TinyCRM.Service.Product.API.Extensions;

public static class Migration
{
    public static async Task ApplyMigrationAsync(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}