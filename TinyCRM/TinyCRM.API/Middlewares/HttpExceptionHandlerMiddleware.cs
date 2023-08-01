using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using TinyCRM.Domain.HttpExceptions;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace TinyCRM.API.Middlewares
{
    public static class HttpExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is HttpException ex)
                    {
                        Log.Error(ex.Message);

                        context.Response.StatusCode = (int)ex.StatusCode;
                        await context.Response.WriteAsJsonAsync(ex.Response);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        var message = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? exceptionHandlerPathFeature?.Error?.Message : "Something went wrong!";

                        Log.Error(exceptionHandlerPathFeature?.Error?.Message ?? string.Empty);

                        await context.Response.WriteAsJsonAsync(new
                        {
                            statusCode = StatusCodes.Status500InternalServerError,
                            code = "INTERNAL",
                            message
                        });
                    }
                });
            });
        }
    }
}