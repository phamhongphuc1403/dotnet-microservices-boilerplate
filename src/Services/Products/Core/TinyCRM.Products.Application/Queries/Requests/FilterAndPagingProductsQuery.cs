using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.Application.Queries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingQuery<Product>, IQuery<FilterAndPagingResultDto<ProductDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto) : base(dto)
    {
    }
}