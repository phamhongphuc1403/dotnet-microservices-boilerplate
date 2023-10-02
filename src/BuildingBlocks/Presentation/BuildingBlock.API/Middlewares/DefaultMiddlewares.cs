using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.API.Middlewares;

public static class DefaultMiddlewares
{
    public static IApplicationBuilder UseDefaultMiddlewares(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseHttpExceptionHandler(env);

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;
    }
}