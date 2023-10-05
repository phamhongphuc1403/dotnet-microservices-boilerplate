using BuildingBlock.API.GRPC;
using BuildingBlock.Application.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.API.Extensions;

public static class AuthenticationExtension
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
}