using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identities.Application.Services;
using TinyCRM.Identities.Application.Services.Interfaces;
using TinyCRM.Identities.EntityFrameworkCore;

namespace Identities.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection RegisterIdentitySeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, IdentitySeeder>();

        return services;
    }

    public static IServiceCollection RegisterIdentityDbContext(this IServiceCollection services)
    {
        services.AddScoped<Func<IdentityAppDbContext>>(provider => () => provider.GetService<IdentityAppDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityAppDbContext>>();

        return services;
    }
}