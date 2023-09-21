using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Extensions;

public static class CqrsExtension
{
    public static IServiceCollection AddCqrs<TApplicationAssemblyReference>(this IServiceCollection services)
        where TApplicationAssemblyReference : class
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(TApplicationAssemblyReference));
        });

        return services;
    }
}