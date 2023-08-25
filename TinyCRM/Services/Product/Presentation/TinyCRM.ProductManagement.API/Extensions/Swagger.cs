using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TinyCRM.Service.Product.API.Extensions;

public static class Swagger
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TinyCRM.Product",
                Version = "v1"
            });
            option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Enter your token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            option.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}