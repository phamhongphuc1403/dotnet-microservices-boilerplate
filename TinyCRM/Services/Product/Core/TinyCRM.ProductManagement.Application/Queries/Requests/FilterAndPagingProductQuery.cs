using TinyCRM.Core;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Application.Queries.Requests;

public class FilterAndPagingProductQuery : FilterAndPagingQuery<Product>, IQuery<FilterAndPagingResultDto<ProductDto>>
{
    public FilterAndPagingProductQuery(FilterAndPagingProductsDto dto) : base(dto)
    {
    }
}