using Microsoft.EntityFrameworkCore;
using BuildingBlock.Domain;
using System.Linq.Dynamic.Core;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications;

namespace BuildingBlock.EntityFrameworkCore;

public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : GuidEntity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;

    public ReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public Task<TEntity?> GetAnyAsync(ISpecification<TEntity> specification,
        string? includeTables = null)
    {
        var query = DbSet.AsNoTracking();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.FirstOrDefaultAsync();
    }

    public Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> specification,
        string? includeTables = null)
    {
        var query = DbSet.AsNoTracking();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.ToListAsync();
    }

    public Task<bool> CheckIfExistAsync(ISpecification<TEntity> specification)
    {
        return DbSet.AsNoTracking().AnyAsync(specification.ToExpression());
    }

    public async Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity> specification, string sort,
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

    private static IQueryable<TEntity> Filter(IQueryable<TEntity> query, ISpecification<TEntity> specification)
    {
        return query.Where(specification.ToExpression());
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