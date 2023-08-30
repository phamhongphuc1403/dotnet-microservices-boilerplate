using System.Linq.Expressions;
using TinyCRM.Core.Constants;
using TinyCRM.Core.DTOs;

namespace TinyCRM.Core;

public class FilterAndPagingQuery<TEntity> where TEntity : GuidBaseEntity
{
    public int Take { get; set; }
    public int Skip { get; set; }
    
    public int Page { get; set; }
    
    public readonly Expression<Func<TEntity, bool>> SearchFilterExpression;

    public readonly string? Sort;

    protected FilterAndPagingQuery(FilterAndPagingDto<TEntity> dto)
    {
        Page = dto.Page ?? Pagination.Page;
        Take = dto.Take ??  Pagination.PageSize;
        Skip = (Page - 1) * Take;
        SearchFilterExpression = dto.BuildSearchExpression();
        Sort = dto.BuildSort();
        if (!string.IsNullOrEmpty(Sort)) Sort += dto.IsDescending == true ? " desc" : "";
    }
}