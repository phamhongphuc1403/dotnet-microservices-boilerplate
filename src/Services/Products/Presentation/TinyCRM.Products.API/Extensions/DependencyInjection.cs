using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Products.Domain.Repositories;
using TinyCRM.Products.EntityFrameworkCore;
using TinyCRM.Products.EntityFrameworkCore.Repositories;

namespace TinyCRM.Service.Product.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
        services.AddScoped<IProductOperationRepository, ProductOperationRepository>();
        
        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<ProductDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<ProductDbContext>>();

        return services;
    }
}