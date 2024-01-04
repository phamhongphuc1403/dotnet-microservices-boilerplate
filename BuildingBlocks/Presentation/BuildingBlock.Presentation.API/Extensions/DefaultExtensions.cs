using System.Text.Json.Serialization;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class DefaultExtensions
{
    public static async Task<IServiceCollection> AddDefaultExtensions<TApplicationAssemblyReference,
        TDomainAssemblyReference, TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DbContext
        where TApplicationAssemblyReference : ApplicationAssemblyReference
        where TDomainAssemblyReference : DomainAssemblyReference
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddApplicationCors(configuration)
            .AddHttpContextAccessor()
            .AddCurrentUser()
            .AddDatabase<TDbContext>(configuration)
            .AddMapper<TDbContext>()
            .AddCqrs<TApplicationAssemblyReference, TDomainAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddEventBus(configuration)
            .AddValidatorsFromAssembly(typeof(TApplicationAssemblyReference).Assembly)
            .AddInMemoryCache(configuration);

        services
            .RegisterSeeders<TApplicationAssemblyReference>()
            .RegisterServices<TApplicationAssemblyReference>()
            .RegisterServices<TDomainAssemblyReference>()
            .RegisterDefaultRepositories<TDomainAssemblyReference, TDbContext>()
            .RegisterCustomRepositories<TDbContext>()
            .RegisterCachedRepositories<TDbContext>()
            .RegisterIntegrationEventHandlers<TApplicationAssemblyReference>()
            .RegisterUnitOfWork<TDbContext>();

        await services.ApplyMigrationAsync<TDbContext>();

        return services;
    }
}