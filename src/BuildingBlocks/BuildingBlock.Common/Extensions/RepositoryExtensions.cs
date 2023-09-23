using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Extensions;

public static class RepositoryExtensions

{
    public static IServiceCollection RegisterRepositories<TEntity, TDbContext>(this IServiceCollection services)
        where TEntity : Entity
        where TDbContext : BaseDbContext
    {
        services.AddScoped<IReadOnlyRepository<TEntity>, ReadOnlyRepository<TDbContext, TEntity>>();
        services.AddScoped<IOperationRepository<TEntity>, OperationRepository<TDbContext, TEntity>>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<TDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        return services;
    }
}