using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Entities;
using TinyCRM.Sales.EntityFrameworkCore;

namespace TinyCRM.Sales.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Deal>, ReadOnlyRepository<SaleDbContext, Deal>>();
        services.AddScoped<IReadOnlyRepository<Lead>, ReadOnlyRepository<SaleDbContext, Lead>>();
        services.AddScoped<IOperationRepository<Lead>, OperationRepository<SaleDbContext, Lead>>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<SaleDbContext>>();

        return services;
    }
}