namespace BuildingBlock.Domain.Repositories;

public interface IOperationRepository<TEntity> where TEntity : GuidEntity
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void Update(TEntity entity);
}