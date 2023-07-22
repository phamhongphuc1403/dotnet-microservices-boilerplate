using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Domain.Middlewares
{
    public class HttpExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        // Populated by dependency injection
        public HttpExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Populated by dependency injection
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)ex.StatusCode;

                await context.Response.WriteAsync(ex.Response);
            }
        }
    }

    public static class HttpExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpExceptionHandlerMiddleware>();
        }
    }
}
