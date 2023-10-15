using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identities.Domain.PermissionAggregate.DomainServices;
using TinyCRM.Identities.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.Common.Services.Implementations;
using TinyCRM.Identity.EntityFrameworkCore;
using TinyCRM.Identity.Identity.Common.Services.Abstractions;
using TinyCRM.Identity.Identity.Common.Services.Implementations;
using TinyCRM.Identity.Identity.PermissionAggregate.DomainServices;
using TinyCRM.Identity.Identity.RoleAggregate.DomainServices;
using TinyCRM.Identity.Identity.UserAggregate.DomainServices;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace Identities.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, IdentityAuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserDomainService, IdentityUserDomainService>();
        services.AddScoped<IPermissionDomainService, IdentityPermissionDomainService>();
        services.AddScoped<IRoleDomainService, IdentityRoleDomainService>();
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