using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications;

public interface ISpecification<TEntity> where TEntity : GuidEntity
{
    Expression<Func<TEntity, bool>> ToExpression();

    Specification<TEntity> And(Specification<TEntity> specification);
    Specification<TEntity> Or(Specification<TEntity> specification);
}