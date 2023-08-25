namespace TinyCRM.Core.BaseRepositories;

public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>, IOperationRepository<TEntity>
    where TEntity : GuidBaseEntity
{
}