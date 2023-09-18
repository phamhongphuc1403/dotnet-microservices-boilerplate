using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications;

public static class ExpressionParameterReplacer
{
    public static Expression ReplaceParameters(Expression expression, ParameterExpression targetParameter)
    {
        return new ParameterReplacer(targetParameter).Visit(expression);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _targetParameter;

        public ParameterReplacer(ParameterExpression targetParameter)
        {
            _targetParameter = targetParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _targetParameter;
        }
    }
}