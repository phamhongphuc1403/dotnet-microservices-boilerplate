using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Params;
using TinyCRM.Domain.Repositories;
using TinyCRM.Infrastructure.Database;

namespace TinyCRM.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : GuidBaseEntity
    {
        private readonly DbFactory _dbFactory;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet => _dbSet ??= _dbFactory.DbContext.Set<TEntity>();

        public Repository(DbFactory dbFactory)
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
                ((IDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public async Task<(List<TEntity>, int)> GetPaginationAsync(PaginationParams<TEntity> parameters)
        {
            var query = DbSet.AsNoTracking();

            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                query = query.OrderBy(parameters.SortBy);
            }

            foreach (var expression in parameters.ExpressionList)
            {
                query = query.Where(expression);
            }

            var totalCount = await query.CountAsync();

            query = query.Skip(parameters.Skip).Take(parameters.Take);

            foreach (string include in parameters.Includes)
            {
                query = query.Include(include);
            }

            return (await query.ToListAsync(), totalCount);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public Task<TEntity?> GetAnyAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, params string[] includes)
        {
            return await GetAnyAsync(entity => entity.Id == id, includes);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = DbSet.AsNoTracking();

            query = query.Where(expression);

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

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
    }
}