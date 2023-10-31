using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingProductsDto,
    IQuery<FilterAndPagingResultDto<ProductSummaryDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto) : base(dto)
    {
        Sort = dto.ConvertSort();
    }

    public string Sort { get; }
}