using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications.Abstractions;

public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : Entity
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public Specification<TEntity> And(Specification<TEntity> specification)
    {
        return new AndSpecification<TEntity>(this, specification);
    }

    public Specification<TEntity> Or(Specification<TEntity> specification)
    {
        return new OrSpecification<TEntity>(this, specification);
    }
}