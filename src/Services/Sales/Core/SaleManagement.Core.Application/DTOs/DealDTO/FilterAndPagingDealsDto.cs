using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.DealDTO.Enums;
using SaleManagement.Core.Domain.DealAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.DTOs.DealDTO;

public class FilterAndPagingDealsDto : FilterAndPagingDto<DealSortProperty>
{
    public DealStatus? Status { get; set; }
}