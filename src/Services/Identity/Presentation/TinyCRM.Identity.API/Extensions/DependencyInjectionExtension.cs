using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identity.Application.Services;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.EntityFrameworkCore;
using TinyCRM.Identity.Indentity.Services.Abstractions;
using TinyCRM.Identity.Indentity.Services.Implementations;

namespace Identities.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, IdentityAuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, IdentityUserService>();
        services.AddScoped<IPermissionService, IdentityPermissionService>();
        services.AddScoped<IRoleService, IdentityRoleService>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }

    public static IServiceCollection RegisterIdentitySeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, IdentitySeeder>();

        return services;
    }

    public static IServiceCollection RegisterIdentityDbContext(this IServiceCollection services)
    {
        services.AddScoped<Func<IdentityDbContext>>(provider => () => provider.GetService<IdentityDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityDbContext>>();

        return services;
    }
}