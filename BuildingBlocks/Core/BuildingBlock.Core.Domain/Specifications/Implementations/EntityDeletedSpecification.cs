using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;

namespace BuildingBlock.Core.Domain.Specifications.Implementations;

public class EntityDeletedSpecification<TEntity> : Specification<TEntity> where TEntity : IEntity
{
    private readonly bool _showDeleted;

    public EntityDeletedSpecification(bool showDeleted)
    {
        _showDeleted = showDeleted;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        if (_showDeleted) return entity => true;

        return entity => entity.DeletedAt == null;
    }
}