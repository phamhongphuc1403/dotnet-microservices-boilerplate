using BuildingBlock.Presentation.API.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddApplicationCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(o => o.AddPolicy(configuration.GetRequiredValue("CORS"), builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        return services;
    }
}