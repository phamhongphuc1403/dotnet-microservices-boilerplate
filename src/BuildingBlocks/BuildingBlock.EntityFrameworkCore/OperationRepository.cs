using Microsoft.EntityFrameworkCore;
using BuildingBlock.Core;
using BuildingBlock.Core.Repositories;

namespace BuildingBlock.EntityFrameworkCore;

public class OperationRepository<TDbContext, TEntity> : IOperationRepository<TEntity> 
    where TDbContext : BaseDbContext
    where TEntity : GuidBaseEntity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;
    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();


    public OperationRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        if (typeof(IDeleteEntity).IsAssignableFrom(typeof(TEntity)))
        {
            (entity as IDeleteEntity)!.IsDeleted = true;
            DbSet.Update(entity);
        }
        else
        {
            DbSet.Remove(entity);
        }
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}