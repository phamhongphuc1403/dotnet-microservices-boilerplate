using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BuildingBlock.Domain;
using System.Linq.Dynamic.Core;
using BuildingBlock.Domain.Repositories;

namespace BuildingBlock.EntityFrameworkCore;

public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity> 
    where TDbContext : BaseDbContext
    where TEntity : GuidBaseEntity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;
    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public ReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, params string[] includes)
    {
        return await GetAnyAsync(entity => entity.Id == id, includes);
    }

    public Task<TEntity?> GetAnyAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = DbSet.AsNoTracking();

        foreach (var include in includes) query = query.Include(include);

        return query.FirstOrDefaultAsync(expression);
    }

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = DbSet.AsNoTracking();

        query = query.Where(expression);

        foreach (var include in includes) query = query.Include(include);

        return query.ToListAsync();
    }

    public Task<bool> CheckIfExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.AsNoTracking().AnyAsync(expression);
    }

    public Task<bool> CheckIfIdExistAsync(Guid id)
    {
        return CheckIfExistAsync(entity => entity.Id == id);
    }

    public async Task<(List<TEntity>, int)> GetFilterAndPagingAsync(FilterAndPagingParams<TEntity> parameters)
    {
        var query = DbSet.AsNoTracking();

        if (!string.IsNullOrEmpty(parameters.SortBy)) query = query.OrderBy(parameters.SortBy);

        foreach (var expression in parameters.ExpressionList) query = query.Where(expression);

        var totalCount = await query.CountAsync();

        query = query.Skip(parameters.Skip).Take(parameters.Take);

        foreach (var include in parameters.Includes) query = query.Include(include);

        return (await query.ToListAsync(), totalCount);
    }
}