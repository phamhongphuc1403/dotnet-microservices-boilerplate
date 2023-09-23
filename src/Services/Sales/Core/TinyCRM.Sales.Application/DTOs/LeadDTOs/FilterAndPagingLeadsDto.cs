using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.LeadDTOs.Enums;
using TinyCRM.Sales.Domain.LeadAggregate.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs.LeadDTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperties>
{
    public LeadStatus? Status { get; set; }
}