using System.Text.Json.Serialization;
using BuildingBlock.Common.Extensions;
using TinyCRM.Products.Application;
using TinyCRM.Products.EntityFrameworkCore;

namespace TinyCRM.Products.API.Extensions;

public static class DefaultExtensions
{
    public async static Task<IServiceCollection> AddDefaultExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services
            .AddDatabase<ProductDbContext>(configuration)
            .AddMapper<Mapper>()
            .AddCqrs<ProductApplicationAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddDependencyInjection()
            .AddEventBus(configuration);
        
        await services.ApplyMigrationAsync<ProductDbContext>();

        return services;
    }
}