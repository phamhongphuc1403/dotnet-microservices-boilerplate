using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<Entity> where Entity : GuidBaseEntity
    {
        void Add(Entity entity);
        void Delete(Entity entity);
        void Update(Entity entity);
        IQueryable<Entity> GetAll();
        Task<Entity?> GetByIdAsync(Guid id, params string[] includes);
        Task<Entity?> GetAnyAsync(Expression<Func<Entity, bool>> expression, params string[] includes);
        Task<List<Entity>> GetPaginationAsync(int? skip, int? take, Expression<Func<Entity, bool>>? expression, string? sortBy, bool? descending, params string[] includes);
    }
}