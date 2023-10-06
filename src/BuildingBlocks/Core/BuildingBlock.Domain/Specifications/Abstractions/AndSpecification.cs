using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications.Abstractions;

public class AndSpecification<TEntity> : Specification<TEntity> where TEntity : Entity
{
    private readonly ISpecification<TEntity> _left;
    private readonly ISpecification<TEntity> _right;

    public AndSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var rightBody =
            ExpressionParameterReplacer.ReplaceParameters(rightExpression.Body, leftExpression.Parameters[0]);

        var body = Expression.AndAlso(leftExpression.Body, rightBody);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(body, leftExpression.Parameters);

        return lambda;
    }
}