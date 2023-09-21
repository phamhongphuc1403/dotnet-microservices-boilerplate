using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications;

public class OrSpecification<TEntity> : Specification<TEntity> where TEntity : GuidEntity
{
    private readonly ISpecification<TEntity> _left;
    private readonly ISpecification<TEntity> _right;

    public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
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

        var body = Expression.OrElse(leftExpression.Body, rightBody);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(body, leftExpression.Parameters);

        return lambda;
    }
}