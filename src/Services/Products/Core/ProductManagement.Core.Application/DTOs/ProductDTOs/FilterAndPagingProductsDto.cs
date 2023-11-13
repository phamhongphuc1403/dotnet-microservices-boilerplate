using BuildingBlock.Core.Application.DTOs;
using ProductManagement.Core.Application.DTOs.ProductDTOs.Enums;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Application.DTOs.ProductDTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<ProductSortProperty>
{
    public ProductType? Type { get; set; }
}