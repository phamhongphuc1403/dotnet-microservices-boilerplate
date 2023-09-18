using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.Enums;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperties>
{
    public LeadStatus? Status { get; set; }
}