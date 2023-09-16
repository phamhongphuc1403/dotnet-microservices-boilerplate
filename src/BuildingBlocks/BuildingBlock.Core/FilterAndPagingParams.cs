using System.Linq.Expressions;

namespace BuildingBlock.Core;

public class FilterAndPagingParams<TEntity> where TEntity : GuidBaseEntity
{
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public List<Expression<Func<TEntity, bool>>> ExpressionList { get; private set; }
    public List<string> Includes { get; private set; }
    public string? SortBy { get; private set; }

    public FilterAndPagingParams(int take, int skip, List<Expression<Func<TEntity, bool>>> expressionList,
        List<string> includes, string? sortBy)
    {
        Take = take;
        Skip = skip;
        ExpressionList = expressionList;
        Includes = includes;
        SortBy = sortBy;
    }
}