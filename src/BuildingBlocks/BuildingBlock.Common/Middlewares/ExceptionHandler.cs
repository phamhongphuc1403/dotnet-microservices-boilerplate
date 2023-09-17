using BuildingBlock.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Common.Middlewares;

public static class ExceptionHandler
{
    public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is BaseException ex)
                {
                    // LoggerService.LogError(ex.Message);

                    context.Response.StatusCode = (int)ex.StatusCode;

                    await context.Response.WriteAsJsonAsync(ex.Response);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var message = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                        ? exceptionHandlerPathFeature?.Error?.Message
                        : "Something went wrong!";

                    // LoggerService.LogError(exceptionHandlerPathFeature?.Error?.Message ?? string.Empty);

                    await context.Response.WriteAsJsonAsync(new
                    {
                        statusCode = StatusCodes.Status500InternalServerError,
                        message
                    });
                }
            });
        });
    }   
}