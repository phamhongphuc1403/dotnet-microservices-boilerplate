using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.ProductDTOs.Enums;
using SaleManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.DTOs.ProductDTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<ProductSortProperty>
{
    public ProductType? Type { get; set; }
}