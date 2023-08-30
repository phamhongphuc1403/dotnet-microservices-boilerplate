using TinyCRM.Core;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Application.Queries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingQuery<Product>, IQuery<FilterAndPagingResultDto<ProductDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto) : base(dto)
    {
    }
}