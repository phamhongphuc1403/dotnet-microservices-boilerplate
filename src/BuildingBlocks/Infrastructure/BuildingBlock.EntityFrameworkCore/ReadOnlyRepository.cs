using System.Linq.Dynamic.Core;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications.Abstractions;
using BuildingBlock.Domain.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : Entity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;

    public ReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public Task<TEntity?> GetAnyAsync(ISpecification<TEntity>? specification = null,
        string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.FirstOrDefaultAsync();
    }

    public Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null,
        string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.ToListAsync();
    }

    public Task<bool> CheckIfExistAsync(ISpecification<TEntity>? specification = null)
    {
        var query = DbSet.AsQueryable();

        query = Filter(query, specification);

        return query.AnyAsync();
    }

    public async Task<(List<TEntity>, int)> GetFilterAndPagingAsync(
        ISpecification<TEntity>? specification,
        string sort,
        int pageIndex,
        int pageSize, string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        query = Filter(query, specification);

        var totalCount = await query.CountAsync();

        query = Include(query, includeTables);

        query = Sort(query, sort);

        query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

        return (await query.ToListAsync(), totalCount);
    }

    private static IQueryable<TEntity> Filter(IQueryable<TEntity> query,
        ISpecification<TEntity>? specification)
    {
        var entityNotDeletedSpecification = new EntityDeletedSpecification<TEntity>(false);

        if (specification == null) return query.Where(entityNotDeletedSpecification.ToExpression());

        var filteredSpecification = specification.And(entityNotDeletedSpecification);

        return query.Where(filteredSpecification.ToExpression());
    }

    private static IQueryable<TEntity> Sort(IQueryable<TEntity> query, string? sort)
    {
        return string.IsNullOrEmpty(sort) ? query.OrderBy("CreatedDate") : query.OrderBy(sort);
    }

    private static IQueryable<TEntity> Include(IQueryable<TEntity> query, string? includeTables = null)
    {
        if (string.IsNullOrEmpty(includeTables)) return query;

        var includeProperties = includeTables.Split(',');

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}