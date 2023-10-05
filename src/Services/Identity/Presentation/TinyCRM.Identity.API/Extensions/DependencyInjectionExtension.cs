using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identity.Application.Services;
using TinyCRM.Identity.Application.Services.Interfaces;
using TinyCRM.Identity.EntityFrameworkCore;

namespace Identities.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

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