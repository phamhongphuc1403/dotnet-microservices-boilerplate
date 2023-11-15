using BuildingBlock.Presentation.API.Extensions;

namespace ProductManagement.Presentation.API.Extensions;

public static class ProductExtensions
{
    public static IServiceCollection AddProductExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);
        services.AddGrpcAuthorization();

        return services;
    }
}