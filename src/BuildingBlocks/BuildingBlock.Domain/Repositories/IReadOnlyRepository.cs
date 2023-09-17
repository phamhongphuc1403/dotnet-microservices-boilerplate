using System.Linq.Expressions;

namespace BuildingBlock.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : GuidBaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, params string[] includes);
    
    Task<TEntity?> GetAnyAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
    
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
    
    Task<bool> CheckIfExistAsync(Expression<Func<TEntity, bool>> expression);

    Task<bool> CheckIfIdExistAsync(Guid id);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(FilterAndPagingParams<TEntity> parameters);
}