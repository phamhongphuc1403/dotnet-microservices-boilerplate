using TinyCRM.Core;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Repositories;
using TinyCRM.SaleManagement.EntityFrameworkCore;
using TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

namespace TinyCRM.SaleManagement.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDealReadOnlyRepository, DealReadOnlyRepository>();
        // services.AddScoped<IDealOperationRepository, DealOperationRepository>();
        
        services.AddScoped<Func<BaseAppDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}