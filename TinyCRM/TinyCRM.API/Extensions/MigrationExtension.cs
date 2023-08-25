using Microsoft.EntityFrameworkCore;
using TinyCRM.EntityFrameworkCore.Data;

namespace TinyCRM.API.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigrationAsync(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}