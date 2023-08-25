namespace TinyCRM.Core.BaseRepositories;

public interface IOperationRepository<TEntity> where TEntity : GuidBaseEntity
{
    void Add(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);
}