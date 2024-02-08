using BuildingBlock.Presentation.API.Extensions;
using IdentityManagement.Core.Application.Shared;
using IdentityManagement.Infrastructure.EntityFrameworkCore;
using IdentityManagement.Infrastructure.Google;
using IdentityManagement.Infrastructure.Identity;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Presentation.API.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExtension(this IServiceCollection services,
        JwtConfiguration jwtConfiguration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromMinutes(30));

        services
            .RegisterDefaultRepositories<IdentityDomainAssemblyReference, AppDbContext>()
            .AddIdentityAuthentication(jwtConfiguration)
            .AddIdentityAuthorization();

        services.AddMapper<GoogleExternalLoginService>();

        return services;
    }
}