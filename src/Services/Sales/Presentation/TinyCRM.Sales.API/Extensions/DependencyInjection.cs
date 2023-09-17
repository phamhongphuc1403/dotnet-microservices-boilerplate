using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Repositories;
using TinyCRM.Sales.EntityFrameworkCore;
using TinyCRM.Sales.EntityFrameworkCore.Repositories;

namespace TinyCRM.Sales.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDealReadOnlyRepository, DealReadOnlyRepository>();
        services.AddScoped<ILeadReadOnlyRepository, LeadReadOnlyRepository>();
        services.AddScoped<ILeadOperationRepository, LeadOperationRepository>();
        
        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<SaleDbContext>>();

        return services;
    }
}