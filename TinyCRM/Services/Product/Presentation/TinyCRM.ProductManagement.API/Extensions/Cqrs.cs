using TinyCRM.ProductManagement.Application;

namespace TinyCRM.Service.Product.API.Extensions;

public static class Cqrs
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(ProductApplicationAssemblyReference));
        });

        return services;
    }
}