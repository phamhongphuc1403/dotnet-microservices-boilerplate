using System.Text.Json.Serialization;
using BuildingBlock.Common.Extensions;
using TinyCRM.Sales.Application;
using TinyCRM.Sales.EntityFrameworkCore;
using TinyCRM.Sales.EntityFrameworkCore.Profiles;

namespace TinyCRM.Sales.API.Extensions;

public static class DefaultExtensions
{
    public static async Task<IServiceCollection> AddDefaultExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddDatabase<SaleDbContext>(configuration)
            .AddMapper<DealProfile>()
            .AddCqrs<SaleApplicationAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddDependencyInjection()
            .AddEventBus(configuration);

        await services.ApplyMigrationAsync<SaleDbContext>();

        return services;
    }
}