using Microsoft.AspNetCore.Authentication.JwtBearer;
using TinyCRM.Infrastructure.JWT.Services;

namespace TinyCRM.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration jwtSettings)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtService.ValidateToken(jwtSettings);
            });

            return services;
        }
    }
}