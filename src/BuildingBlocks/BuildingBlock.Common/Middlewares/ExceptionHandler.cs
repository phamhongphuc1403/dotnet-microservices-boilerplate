using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using BuildingBlock.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Common.Middlewares;

public static class ExceptionHandler
{
    public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
    {
        return app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                var response = context.Response;

                var isDevelopment = env.IsDevelopment();

                var statusCode = GetStatusCode(exception);

                response.StatusCode = statusCode;
                response.ContentType = "application/json";

                var pd = new ProblemDetails
                {
                    Title = GetMessage(exception, statusCode, isDevelopment),
                    Status = statusCode,
                    Detail = isDevelopment ? exception?.StackTrace : null
                };

                // if (exception is ValidationException validationException)
                //     pd.Extensions.Add("errors",
                //         validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

                pd.Extensions.Add("traceId", context.TraceIdentifier);

                await context.Response.WriteAsync(JsonSerializer.Serialize(pd));
            });
        });
    }

    private static int GetStatusCode(Exception? ex)
    {
        var statusCode = ex switch
        {
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            NotImplementedException => (int)HttpStatusCode.NotImplemented,
            EntityConflictException => (int)HttpStatusCode.Conflict,
            ValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return statusCode;
    }

    private static string? GetMessage(Exception? exception, int statusCode, bool isDevelopment)
    {
        return exception is ValidationException ? "Validation error" :
            isDevelopment && statusCode == 500 ? "An error occurred on the server." : exception?.Message;
    }
}