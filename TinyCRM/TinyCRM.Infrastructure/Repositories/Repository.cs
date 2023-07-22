using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.Database;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace TinyCRM.Infrastructure.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : GuidBaseEntity
    {
        private readonly DbFactory _dbFactory;
        private DbSet<Entity> _dbSet;

        protected DbSet<Entity> DbSet
        {
            get => _dbSet ??= _dbFactory.DbContext.Set<Entity>();
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Add(Entity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(Entity entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(Entity)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public Task<List<Entity>> GetPaginationAsync(int? skip, int? take, Expression<Func<Entity, bool>>? expression, string? sortBy, bool? descending, params string[] includes)
        {
            var query = DbSet.AsNoTracking();

            if (!string.IsNullOrEmpty(sortBy))
            {
                var properties = typeof(Entity).GetProperties();

                if (!properties.Any(property => property.ToString() == sortBy))
                {
                    sortBy = "Id";
                }

                sortBy += descending == true ? " desc" : "";

                query = query.OrderBy(sortBy);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }
            
            query = query.Skip(skip ?? 0).Take(take ?? 10);

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query.ToListAsync();
        }

        public void Update(Entity entity)
        {
            DbSet.Update(entity);
        }

        public Task<Entity?> GetAnyAsync(Expression<Func<Entity, bool>> expression, params string[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefaultAsync(expression);
        }

        public async Task<Entity?> GetByIdAsync(Guid id, params string[] includes)
        {
            return await GetAnyAsync(entity => entity.Id == id, includes);
        }

        public Task<List<Entity>> GetAllAsync(Expression<Func<Entity, bool>> expression, params string[] includes)
        {
            var query = DbSet.AsNoTracking();

            query.Where(expression);

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query.ToListAsync();
        }
    }
}