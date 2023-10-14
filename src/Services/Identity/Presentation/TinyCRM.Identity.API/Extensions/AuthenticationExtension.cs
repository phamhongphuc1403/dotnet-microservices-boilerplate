using Microsoft.AspNetCore.Authentication.JwtBearer;
using TinyCRM.Identity.Application;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace Identities.API.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
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
            options.TokenValidationParameters = tokenService.ValidateToken(jwtSetting.AccessTokenSecurityKey);
        });

        return services;
    }
}