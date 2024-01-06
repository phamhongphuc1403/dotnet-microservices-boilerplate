using BuildingBlock.Presentation.API.Middlewares;
using BuildingBlock.Presentation.API.Utilities;

namespace Shared.Presentation.API.Middlewares;

public static class SharedMiddlewares
{
    public static async Task<IApplicationBuilder> UseSharedMiddlewaresAsync(this IApplicationBuilder app,
        IHostEnvironment env, IConfiguration configuration)
    {
        app.UseHttpExceptionHandler(env);

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseCors(configuration.GetRequiredValue("CORS"));

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        await app.SeedDataAsync();

        return app;
    }
}