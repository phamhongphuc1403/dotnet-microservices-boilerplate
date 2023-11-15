namespace BuildingBlock.Core.Domain.Repositories;

public interface IOperationRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
{
    Task AddAsync(TAggregateRoot entity);

    Task AddRangeAsync(IEnumerable<TAggregateRoot> entities);

    void Delete(TAggregateRoot entity);

    void DeleteRange(IEnumerable<TAggregateRoot> entities);

    void Update(TAggregateRoot entity);
}