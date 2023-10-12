using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using BuildingBlock.Domain.Exceptions;
using BuildingBlocks.Identity.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.API.Middlewares;

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

                var pd = GetResponseBody(exception, statusCode, isDevelopment, context);

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
            AuthenticationException => (int)HttpStatusCode.Unauthorized,
            UnauthorizedException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return statusCode;
    }

    private static string? GetMessage(Exception? exception, int statusCode, bool isDevelopment)
    {
        return exception is ValidationException ? "Validation error" :
            exception is IdentityException ? "Identity error" :
            !isDevelopment && statusCode == 500 ? "An error occurred on the server." : exception?.Message;
    }

    private static ProblemDetails GetResponseBody(Exception? exception, int statusCode, bool isDevelopment,
        HttpContext context)
    {
        var message = GetMessage(exception, statusCode, isDevelopment);

        var stackTrace = isDevelopment ? exception?.StackTrace : null;

        var pd = new ProblemDetails
        {
            Title = message,
            Status = statusCode,
            Detail = stackTrace
        };

        switch (exception)
        {
            case ValidationException validationException:
                pd.Extensions.Add("errors",
                    validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
                break;
            case IdentityException identityException:
                pd.Extensions.Add("errors",
                    identityException.Errors.Select(x => new { x.Code, x.Description }));
                break;
        }

        pd.Extensions.Add("traceId", context.TraceIdentifier);

        return pd;
    }
}