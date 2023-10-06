using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.API.Extensions;

public static class RepositoryExtensions

{
    public static IServiceCollection RegisterRepositories<TAggregateRoot, TDbContext>(this IServiceCollection services)
        where TAggregateRoot : AggregateRoot
        where TDbContext : BaseDbContext
    {
        services.AddScoped<IReadOnlyRepository<TAggregateRoot>, ReadOnlyRepository<TDbContext, TAggregateRoot>>();
        services.AddScoped<IOperationRepository<TAggregateRoot>, OperationRepository<TDbContext, TAggregateRoot>>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<TDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        return services;
    }
}