using BuildingBlock.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Middlewares;

public static class SeedDataMiddleware
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<IDataSeeder>>();

        foreach (var seeder in seeders) await seeder.SeedDataAsync();
    }
}