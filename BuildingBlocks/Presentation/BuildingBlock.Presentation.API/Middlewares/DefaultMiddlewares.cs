using BuildingBlock.Core.Application;
using BuildingBlock.Presentation.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class DefaultMiddlewares
{
    public static async Task<IApplicationBuilder> UseDefaultMiddlewares<TApplicationAssemblyReference>(
        this IApplicationBuilder app,
        IHostEnvironment env, IConfiguration configuration)
        where TApplicationAssemblyReference : ApplicationAssemblyReference
    {
        app.UseHttpExceptionHandler(env);

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseCors(configuration.GetRequiredValue("CORS"));

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        await app.SeedDataAsync();

        app.RegisterEventBusSubcriptions<TApplicationAssemblyReference>();

        return app;
    }
}