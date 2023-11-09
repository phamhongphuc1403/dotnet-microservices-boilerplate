using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlock.API.Extensions;

public static class DefaultOpenApiExtension
{
    public static IServiceCollection AddDefaultOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        var openApi = configuration.GetSection("OpenApi");

        if (!openApi.Exists()) return services;

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

            option.OperationFilter<IgnorePropertyFilter>();

            option.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}

public class IgnorePropertyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription == null || operation.Parameters == null)
            return;

        if (!context.ApiDescription.ParameterDescriptions.Any())
            return;

        var formParametersToRemove = context.ApiDescription.ParameterDescriptions
            .Where(p => p.Source.Equals(BindingSource.Form) &&
                        p.CustomAttributes().Any(attr => attr is JsonIgnoreAttribute)
            ).ToList();

        var queryParametersToRemove = context.ApiDescription.ParameterDescriptions
            .Where(p => p.Source.Equals(BindingSource.Query) &&
                        p.CustomAttributes().Any(attr => attr is JsonIgnoreAttribute)
            ).ToList();

        foreach (var parameter in queryParametersToRemove.Select(parameterToRemove =>
                     operation.Parameters.Single(p => p.Name.Equals(parameterToRemove.Name))))
            operation.Parameters.Remove(parameter);
    }
}