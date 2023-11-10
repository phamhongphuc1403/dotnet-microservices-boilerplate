using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class DefaultMiddlewares
{
    public static async Task<IApplicationBuilder> UseDefaultMiddlewares(this IApplicationBuilder app,
        IHostEnvironment env)
    {
        app.UseHttpExceptionHandler(env);

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        await app.SeedDataAsync();

        return app;
    }
}