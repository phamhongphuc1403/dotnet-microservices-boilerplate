using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingProductsDto, IQuery<FilterAndPagingResultDto<ProductDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        Type = dto.Type;
    }

    public string Sort { get; private init; }
}