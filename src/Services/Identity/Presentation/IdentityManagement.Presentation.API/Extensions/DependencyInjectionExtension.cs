using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using IdentityManagement.Core.Application.Common.Services.Abstractions;
using IdentityManagement.Core.Application.Common.Services.Implementations;
using IdentityManagement.Core.Application.Seeder;
using Identitymanagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.PermissionAggregate.DomainServices.Implementations;
using Identitymanagement.Core.Domain.PermissionAggregate.Repositories;
using Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Implementations;
using Identitymanagement.Core.Domain.RoleAggregate.Repositories;
using Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.UserAggregate.DomainServices.Implementations;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Infrastructure.EntityFrameworkCore;
using IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;
using IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedRoleRepositories;
using IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.PermissionRepositories;
using IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.RoleRepositories;
using IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.UserRepositories;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Presentation.API.Extensions;

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