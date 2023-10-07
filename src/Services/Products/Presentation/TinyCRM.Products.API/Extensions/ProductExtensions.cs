using BuildingBlock.API.Extensions;
using BuildingBlock.Application;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Products.Application;
using TinyCRM.Products.Domain.ProductAggregate.DomainServices;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.EntityFrameworkCore;

namespace TinyCRM.Products.API.Extensions;

public static class ProductExtensions
{
    public static IServiceCollection AddProductExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);

        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<ProductDbContext, Product>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<ProductDbContext, Product>>();

        services.AddScoped<IDataSeeder, ProductSeeder>();

        services.AddScoped<IProductDomainService, ProductDomainService>();

        return services;
    }
}