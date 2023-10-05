using BuildingBlock.API.Authentication;
using BuildingBlock.API.Authorization;
using BuildingBlock.API.GRPC;
using BuildingBlock.API.GRPC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.API.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }

    public static IServiceCollection AddGrpcAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        // "GrpcUrls" : {
        //     "Identity": ""
        // }

        services.AddGrpcClient<AuthProvider.AuthProviderClient>((_, options) =>
        {
            var identityUrl = configuration.GetRequiredValue("GrpcUrls:Identity");
            options.Address = new Uri(identityUrl);
        });

        services.AddAuthentication(AuthenticationDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, GrpcAuthenticationHandler>(
                AuthenticationDefaults.AuthenticationScheme, null);

        return services;
    }

    public static IServiceCollection AddGrpcAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, GrpcPermissionAuthorizationHandler>();

        return services;
    }
}