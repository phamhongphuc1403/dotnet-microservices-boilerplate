using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : GuidBaseEntity
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<TEntity?> GetByIdAsync(Guid id, params string[] includes);

        Task<TEntity?> GetAnyAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);

        Task<bool> CheckIfExistAsync(Expression<Func<TEntity, bool>> expression);

        Task<bool> CheckIfIdExistAsync(Guid id);

        Task<(List<TEntity>, int)> GetPaginationAsync(PaginationParams<TEntity> parameters);
    }
}