using Microsoft.EntityFrameworkCore;
using TinyCRM.Core;
using TinyCRM.Core.Repositories;

namespace TinyCRM.EntityFrameworkCore;

public class OperationRepository<TEntity> : IOperationRepository<TEntity> where TEntity : GuidBaseEntity
{
    private readonly DbFactory _dbFactory;
    private DbSet<TEntity> _dbSet = null!;
    protected DbSet<TEntity> DbSet => _dbSet ??= _dbFactory.DbContext.Set<TEntity>();

    public OperationRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
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