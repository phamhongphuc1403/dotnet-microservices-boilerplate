using System.Linq.Dynamic.Core;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications.Abstractions;
using BuildingBlock.Domain.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class ReadOnlyRepository<TDbContext, TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
    where TDbContext : BaseDbContext
    where TAggregateRoot : AggregateRoot
{
    private readonly TDbContext _dbContext;
    private DbSet<TAggregateRoot>? _dbSet;

    public ReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TAggregateRoot> DbSet => _dbSet ??= _dbContext.Set<TAggregateRoot>();

    public Task<TAggregateRoot?> GetAnyAsync(ISpecification<TAggregateRoot> specification,
        string? includeTables = null)
    {
        var query = DbSet.AsNoTracking();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.FirstOrDefaultAsync();
    }

    public Task<List<TAggregateRoot>> GetAllAsync(ISpecification<TAggregateRoot> specification,
        string? includeTables = null)
    {
        var query = DbSet.AsNoTracking();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.ToListAsync();
    }

    public Task<bool> CheckIfExistAsync(ISpecification<TAggregateRoot> specification)
    {
        return DbSet.AsNoTracking().AnyAsync(specification.ToExpression());
    }

    public async Task<(List<TAggregateRoot>, int)> GetFilterAndPagingAsync(ISpecification<TAggregateRoot> specification,
        string sort,
        int pageIndex,
        int pageSize, string? includeTables = null)
    {
        var query = DbSet.AsNoTracking();

        query = Filter(query, specification);

        var totalCount = await query.CountAsync();

        query = Include(query, includeTables);

        query = Sort(query, sort);

        query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

        return (await query.ToListAsync(), totalCount);
    }

    private static IQueryable<TAggregateRoot> Filter(IQueryable<TAggregateRoot> query,
        ISpecification<TAggregateRoot> specification)
    {
        var entityNotDeletedSpecification = new EntityDeletedSpecification<TAggregateRoot>(false);

        var filteredSpecification = specification.And(entityNotDeletedSpecification);

        return query.Where(filteredSpecification.ToExpression());
    }

    private static IQueryable<TAggregateRoot> Sort(IQueryable<TAggregateRoot> query, string? sort)
    {
        return string.IsNullOrEmpty(sort) ? query.OrderBy("CreatedDate") : query.OrderBy(sort);
    }

    private static IQueryable<TAggregateRoot> Include(IQueryable<TAggregateRoot> query, string? includeTables = null)
    {
        if (string.IsNullOrEmpty(includeTables)) return query;

        var includeProperties = includeTables.Split(',');

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}