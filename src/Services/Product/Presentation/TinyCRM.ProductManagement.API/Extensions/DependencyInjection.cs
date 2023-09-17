using BuildingBlock.Core;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Repositories;
using TinyCRM.ProductManagement.EntityFrameworkCore;
using TinyCRM.ProductManagement.EntityFrameworkCore.Repositories;

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