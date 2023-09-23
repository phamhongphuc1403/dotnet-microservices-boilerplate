using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class OperationRepository<TDbContext, TEntity> : IOperationRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : Entity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;

    public OperationRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var guidEntities = entities.ToList();

        await DbSet.AddRangeAsync(guidEntities);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}