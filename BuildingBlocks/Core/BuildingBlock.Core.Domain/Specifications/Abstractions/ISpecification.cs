using System.Linq.Expressions;
using AutoMapper;

namespace BuildingBlock.Core.Domain.Specifications.Abstractions;

public interface ISpecification<TEntity> where TEntity : IEntity
{
    Expression<Func<TEntity, bool>> ToExpression();

    Specification<TEntity> And(Specification<TEntity> specification);

    Specification<TEntity> Or(Specification<TEntity> specification);

    Specification<T> ConvertTo<T>(IMapper mapper) where T : IEntity;
}