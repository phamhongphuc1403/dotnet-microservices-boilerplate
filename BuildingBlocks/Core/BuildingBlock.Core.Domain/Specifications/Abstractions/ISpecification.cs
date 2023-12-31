using System.Linq.Expressions;

namespace BuildingBlock.Core.Domain.Specifications.Abstractions;

public interface ISpecification<TEntity> where TEntity : IEntity
{
    Expression<Func<TEntity, bool>> ToExpression();

    Specification<TEntity> And(Specification<TEntity> specification);
    Specification<TEntity> Or(Specification<TEntity> specification);
}