using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Application.Services.Implementations;
using TinyCRM.Identity.EntityFrameworkCore;
using TinyCRM.Identity.Identity.Entities;
using TinyCRM.Identity.Identity.Services.Abstractions;
using TinyCRM.Identity.Identity.Services.Implementations;

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

    public static IServiceCollection RegisterIdentityRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<ApplicationUser>, ReadOnlyRepository<AppDbContext, ApplicationUser>>();

        return services;
    }

    public static IServiceCollection RegisterIdentityDbContext(this IServiceCollection services)
    {
        services.AddScoped<Func<AppDbContext>>(provider => () => provider.GetService<AppDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        return services;
    }
}