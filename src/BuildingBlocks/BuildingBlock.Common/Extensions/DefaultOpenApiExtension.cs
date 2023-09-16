using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BuildingBLock.Common.Extensions;

public static class DefaultOpenApiExtension
{
    public static IServiceCollection AddDefaultOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        var openApi = configuration.GetSection("OpenApi");

        if (!openApi.Exists())
        {
            return services;
        }
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(option =>
        {
            var document = openApi.GetRequiredSection("Document");
            var title = $"TinyCRM.{document["Title"]}";
            var version = document["Version"] ?? "v1";
            
            option.SwaggerDoc(version, new OpenApiInfo
            {
                Title = title,
                Version = version
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