using BuildingBlock.Application.DTOs;
using TinyCRM.Products.Application.DTOs.Enums;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;

namespace TinyCRM.Products.Application.DTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<ProductSortProperty>
{
    public ProductType? Type { get; set; }
}