using BuildingBlock.Core.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class SeedDataMiddleware
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<IDataSeeder>>().ToList();

        var orderedSeeders = seeders.OrderBy(seeder => seeder.ExecutionOrder);

        foreach (var seeder in orderedSeeders) await seeder.SeedDataAsync();
    }
}