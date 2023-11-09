using System.Text.Json.Serialization;
using BuildingBlock.Core.Application;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class DefaultExtensions
{
    public static async Task<IServiceCollection> AddDefaultExtensions<TDbContext, TApplicationAssemblyReference>(
        this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DbContext
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
            .AddCurrentUser()
            .AddDatabase<TDbContext>(configuration)
            .AddMapper<TDbContext>()
            .AddCqrs<TApplicationAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddEventBus(configuration)
            .AddValidatorsFromAssembly(typeof(TApplicationAssemblyReference).Assembly)
            .AddInMemoryCache(configuration);

        await services.ApplyMigrationAsync<TDbContext>();

        return services;
    }
}