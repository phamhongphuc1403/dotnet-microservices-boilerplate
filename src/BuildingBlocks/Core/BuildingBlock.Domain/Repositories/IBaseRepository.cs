namespace BuildingBlock.Domain.Repositories;

public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>, IOperationRepository<TEntity>
    where TEntity : Entity
{
}