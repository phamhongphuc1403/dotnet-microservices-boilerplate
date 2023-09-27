using System.Text.Json.Serialization;
using BuildingBlock.Application;
using BuildingBlock.EntityFrameworkCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Extensions;

public static class DefaultExtensions
{
    public static async Task<IServiceCollection> AddDefaultExtensions<TDbContext, TApplicationAssemblyReference>(
        this IServiceCollection services, IConfiguration configuration)
        where TDbContext : BaseDbContext
        where TApplicationAssemblyReference : ApplicationAssemblyReference
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddHttpContextAccessor()
            .AddDatabase<TDbContext>(configuration)
            .AddMapper<TDbContext>()
            .AddCqrs<TApplicationAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddEventBus(configuration)
            .AddValidatorsFromAssembly(typeof(TApplicationAssemblyReference).Assembly);


        await services.ApplyMigrationAsync<TDbContext>();

        return services;
    }
}