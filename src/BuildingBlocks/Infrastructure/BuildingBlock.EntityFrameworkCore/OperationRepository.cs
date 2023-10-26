using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class OperationRepository<TDbContext, TAggregateRoot> : IOperationRepository<TAggregateRoot>
    where TDbContext : DbContext
    where TAggregateRoot : class, IAggregateRoot
{
    private readonly TDbContext _dbContext;
    private DbSet<TAggregateRoot>? _dbSet;

    public OperationRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TAggregateRoot> DbSet => _dbSet ??= _dbContext.Set<TAggregateRoot>();

    public async Task AddAsync(TAggregateRoot entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TAggregateRoot> entities)
    {
        var guidEntities = entities.ToList();

        await DbSet.AddRangeAsync(guidEntities);
    }

    public void Remove(TAggregateRoot entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TAggregateRoot> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public void Update(TAggregateRoot entity)
    {
        DbSet.Update(entity);
    }
}