using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using BuildingBlock.Presentation.API.Extensions;
using SaleManagement.Core.Application.IntegrationEvents.Handlers;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Implementations;
using SaleManagement.Core.Domain.ProductAggregate.Entities;
using SaleManagement.Infrastructure.EntityFrameworkCore;

namespace SaleManagement.Presentation.API.Extensions;

public static class SaleExtensions
{
    public static IServiceCollection AddSaleExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcAuthentication(configuration);
        services.AddGrpcAuthorization();

        services.AddTransient<ProductCreatedIntegrationEventHandler>();
        services.AddTransient<ProductEditedIntegrationEventHandler>();
        services.AddTransient<ProductDeletedIntegrationEventHandler>();
        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<SaleDbContext, Product>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<SaleDbContext, Product>>();
        services.AddScoped<IProductDomainService, ProductDomainService>();
        return services;
    }
}