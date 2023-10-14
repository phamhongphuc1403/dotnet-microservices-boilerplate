using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.Application;
using TinyCRM.Identity.EntityFrameworkCore;
using TinyCRM.Identity.Identity.Entities;

namespace Identities.API.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExtension(this IServiceCollection services, JwtSetting jwtSetting)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
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

        services
            .RegisterIdentitySeeder()
            .RegisterIdentityDbContext()
            .RegisterIdentityServices()
            ;

        services.AddIdentityAuthentication(jwtSetting);
        services.AddIdentityAuthorization();
        return services;
    }
}