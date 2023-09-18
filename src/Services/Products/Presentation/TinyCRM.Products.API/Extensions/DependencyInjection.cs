using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.EntityFrameworkCore;

namespace TinyCRM.Products.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<ProductDbContext, Product>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<ProductDbContext, Product>>();
        
        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<ProductDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<ProductDbContext>>();

        return services;
    }
}