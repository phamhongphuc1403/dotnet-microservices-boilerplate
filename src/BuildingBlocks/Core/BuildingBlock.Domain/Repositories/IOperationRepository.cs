namespace BuildingBlock.Domain.Repositories;

public interface IOperationRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
{
    Task AddAsync(TAggregateRoot entity);

    Task AddRangeAsync(IEnumerable<TAggregateRoot> entities);

    void Remove(TAggregateRoot entity);

    void RemoveRange(IEnumerable<TAggregateRoot> entities);

    void Update(TAggregateRoot entity);
}