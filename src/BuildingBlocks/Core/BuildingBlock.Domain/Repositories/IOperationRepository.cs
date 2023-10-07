namespace BuildingBlock.Domain.Repositories;

public interface IOperationRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
}