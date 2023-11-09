using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;

public record FilterAndPagingProductsQuery
    (FilterAndPagingProductsDto Dto) : IQuery<FilterAndPagingResultDto<ProductSummaryDto>>
{
}