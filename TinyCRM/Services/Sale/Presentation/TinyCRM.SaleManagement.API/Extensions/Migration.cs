using Microsoft.EntityFrameworkCore;
using TinyCRM.SaleManagement.EntityFrameworkCore;

namespace TinyCRM.SaleManagement.API.Extensions;

public static class Migration
{
    public static async Task ApplyMigrationAsync(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SaleDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}