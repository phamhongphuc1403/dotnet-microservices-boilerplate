using BuildingBlock.API.Authorization;
using Identity.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Extensions;

public static class AuthorizationExtension
{
    public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, IdentityPermissionAuthorizationHandler>();

        return services;
    }
}