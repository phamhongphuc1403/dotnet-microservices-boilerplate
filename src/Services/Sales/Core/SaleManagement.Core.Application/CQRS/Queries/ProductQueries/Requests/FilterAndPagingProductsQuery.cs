using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.ProductDTOs;

namespace SaleManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;

public record FilterAndPagingProductsQuery
    (FilterAndPagingProductsDto Dto) : IQuery<FilterAndPagingResultDto<ProductSummaryDto>>;