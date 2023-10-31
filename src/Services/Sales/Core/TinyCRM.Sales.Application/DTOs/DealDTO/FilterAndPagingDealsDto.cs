using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.DealDTO.Enums;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs.DealDTO;

public class FilterAndPagingDealsDto : FilterAndPagingDto<DealSortProperty>
{
    protected FilterAndPagingDealsDto(FilterAndPagingDealsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Status = dto.Status;
    }

    public FilterAndPagingDealsDto()
    {
    }

    public DealStatus? Status { get; set; }
}