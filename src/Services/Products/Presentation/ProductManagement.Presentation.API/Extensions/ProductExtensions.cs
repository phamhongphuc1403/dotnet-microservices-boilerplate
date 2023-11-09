using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using BuildingBlock.Presentation.API.Extensions;
using ProductManagement.Core.Application;
using ProductManagement.Core.Domain.ProductAggregate.DomainServices;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Infrastructure.EntityFrameworkCore;

namespace ProductManagement.Presentation.API.Extensions;

public static class ProductExtensions
{
    public static IServiceCollection AddProductExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);
        services.AddGrpcAuthorization();

        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<ProductDbContext, Product>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<ProductDbContext, Product>>();

        services.AddScoped<IDataSeeder, ProductSeeder>();

        services.AddScoped<IProductDomainService, ProductDomainService>();

        return services;
    }
}