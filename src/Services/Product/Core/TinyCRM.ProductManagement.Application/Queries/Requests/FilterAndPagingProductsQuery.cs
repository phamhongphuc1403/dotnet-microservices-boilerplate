using BuildingBlock.Core;
using BuildingBlock.Core.CQRS;
using BuildingBlock.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Application.Queries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingQuery<Product>, IQuery<FilterAndPagingResultDto<ProductDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto) : base(dto)
    {
    }
}