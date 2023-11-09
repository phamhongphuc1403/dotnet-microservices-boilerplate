using BuildingBlock.Application;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Services;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.Common.Services.Implementations;
using TinyCRM.Identity.Application.Seeder;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Implementations;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices.Implementations;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Implementations;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;
using TinyCRM.Identity.EntityFrameworkCore;
using TinyCRM.Identity.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;
using TinyCRM.Identity.EntityFrameworkCore.CachedRepositories.CachedRoleRepositories;
using TinyCRM.Identity.EntityFrameworkCore.Repositories.PermissionRepositories;
using TinyCRM.Identity.EntityFrameworkCore.Repositories.RoleRepositories;
using TinyCRM.Identity.EntityFrameworkCore.Repositories.UserRepositories;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace Identity.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<IPermissionDomainService, PermissionDomainService>();
        services.AddScoped<IRoleDomainService, RoleDomainService>();

        return services;
    }

    public static IServiceCollection RegisterIdentitySeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, RoleSeeder>();
        services.AddScoped<IDataSeeder, PermissionSeeder>();
        services.AddScoped<IDataSeeder, UserSeeder>();
        services.AddScoped<IDataSeeder, UserRoleSeeder>();

        return services;
    }

    public static IServiceCollection RegisterIdentityRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<ApplicationUser>, ReadOnlyRepository<AppDbContext, ApplicationUser>>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
        services.AddScoped<IUserOperationRepository, UserOperationRepository>();
        services
            .AddScoped<IReadOnlyRepository<ApplicationRefreshToken>,
                ReadOnlyRepository<AppDbContext, ApplicationRefreshToken>>();
        services.AddScoped<IRefreshTokenReadOnlyRepository, RefreshTokenReadOnlyRepository>();

        services
            .AddScoped<IReadOnlyRepository<ApplicationPermission>,
                ReadOnlyRepository<AppDbContext, ApplicationPermission>>();
        services.AddScoped<IPermissionReadOnlyRepository, PermissionReadOnlyRepository>();
        services.AddScoped<IPermissionOperationRepository, PermissionOperationRepository>();

        services.Decorate<IPermissionOperationRepository, CachedPermissionOperationRepository>();
        services.Decorate<IPermissionReadOnlyRepository, CachedPermissionReadOnlyRepository>();

        services.AddScoped<IReadOnlyRepository<ApplicationRole>, ReadOnlyRepository<AppDbContext, ApplicationRole>>();
        services.AddScoped<IRoleReadOnlyRepository, RoleReadOnlyRepository>();
        services.AddScoped<IRoleOperationRepository, RoleOperationRepository>();

        services.Decorate<IRoleReadOnlyRepository, CachedRoleReadOnlyRepository>();
        services.Decorate<IRoleOperationRepository, CachedRoleOperationRepository>();

        services
            .AddScoped<IReadOnlyRepository<ApplicationUserRole>,
                ReadOnlyRepository<AppDbContext, ApplicationUserRole>>();
        services.AddScoped<IUserRoleReadOnlyRepository, UserRoleReadOnlyRepository>();

        return services;
    }

    public static IServiceCollection RegisterIdentityDbContext(this IServiceCollection services)
    {
        services.AddScoped<Func<AppDbContext>>(provider => () => provider.GetService<AppDbContext>()!);
        services.AddScoped<IUnitOfWork, IdentityUnitOfWork>();

        return services;
    }
}