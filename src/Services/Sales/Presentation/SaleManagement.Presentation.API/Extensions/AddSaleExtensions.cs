using BuildingBlock.Presentation.API.Extensions;

namespace SaleManagement.Presentation.API.Extensions;

public static class SaleExtensions
{
    public static IServiceCollection AddSaleExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);
        services.AddGrpcAuthorization();

        return services;
    }
}