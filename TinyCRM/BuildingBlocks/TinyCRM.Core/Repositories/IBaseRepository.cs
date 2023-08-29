namespace TinyCRM.Core.Repositories;

public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>, IOperationRepository<TEntity>
    where TEntity : GuidBaseEntity
{
}