using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.LeadDTOs.Enums;
using SaleManagement.Core.Domain.LeadAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.DTOs.LeadDTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperty>
{
    public LeadStatus? Status { get; set; }
}