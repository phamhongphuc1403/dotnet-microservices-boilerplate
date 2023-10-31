using BuildingBlock.Application.DTOs;
using TinyCRM.Products.Application.DTOs.Enums;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;

namespace TinyCRM.Products.Application.DTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<ProductSortProperty>
{
    protected FilterAndPagingProductsDto(FilterAndPagingProductsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Type = dto.Type;
    }

    public FilterAndPagingProductsDto()
    {
    }

    public ProductType? Type { get; set; }
}