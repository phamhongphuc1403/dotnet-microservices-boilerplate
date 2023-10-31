using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.LeadDTOs.Enums;
using TinyCRM.Sales.Domain.LeadAggregate.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs.LeadDTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperty>
{
    protected FilterAndPagingLeadsDto(FilterAndPagingLeadsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Status = dto.Status;
    }

    public FilterAndPagingLeadsDto()
    {
    }

    public LeadStatus? Status { get; set; }
}