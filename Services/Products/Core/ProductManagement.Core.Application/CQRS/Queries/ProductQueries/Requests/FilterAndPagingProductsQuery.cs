using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using ProductManagement.Core.Application.DTOs.ProductDTOs;

namespace ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;

public record FilterAndPagingProductsQuery
    (FilterAndPagingProductsDto Dto) : IQuery<FilterAndPagingResultDto<ProductSummaryDto>>;