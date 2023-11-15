using BuildingBlock.Presentation.API.Authorization;
using IdentityManagement.Presentation.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace IdentityManagement.Presentation.API.Extensions;

public static class AuthorizationExtension
{
    public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, IdentityPermissionAuthorizationHandler>();

        return services;
    }
}