using System.Linq.Expressions;

namespace TinyCRM.Core;

public class FilterAndPagingBuilder<TEntity> where TEntity : GuidBaseEntity
{
    private readonly List<Expression<Func<TEntity, bool>>> _expressionList = new();
    private readonly List<string> _includes;
    private readonly FilterAndPagingQuery<TEntity> _query;

    private FilterAndPagingBuilder(FilterAndPagingQuery<TEntity> query)
    {
        _query = query;
        _includes = new List<string>();
        _expressionList.Add(_query.SearchFilterExpression);
    }

    public static FilterAndPagingBuilder<TEntity> Init(FilterAndPagingQuery<TEntity> query)
    {
        return new FilterAndPagingBuilder<TEntity>(query);
    }

    public FilterAndPagingBuilder<TEntity> JoinTable(string table)
    {
        _includes.Add(table);
        return this;
    }

    public FilterAndPagingBuilder<TEntity> AddConstraint(Expression<Func<TEntity, bool>> expression)
    {
        _expressionList.Add(expression);

        return this;
    }

    public FilterAndPagingParams<TEntity> Build()
    {
        return new FilterAndPagingParams<TEntity>(_query.Take, _query.Skip, _expressionList, _includes, _query.Sort);
    }
}