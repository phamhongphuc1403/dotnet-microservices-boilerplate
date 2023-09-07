using TinyCRM.SaleManagement.Application;

namespace TinyCRM.SaleManagement.API.Extensions;

public static class Cqrs
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(SaleApplicationAssemblyReference));
        });

        return services;
    }
}