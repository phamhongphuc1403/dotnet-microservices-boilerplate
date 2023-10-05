using Microsoft.AspNetCore.Authentication.JwtBearer;
using TinyCRM.Identity.Application.Services.Interfaces;

namespace Identities.API.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services)
    {
        var tokenService = services.BuildServiceProvider().GetRequiredService<ITokenService>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = tokenService.ValidateToken();
        });

        return services;
    }
}