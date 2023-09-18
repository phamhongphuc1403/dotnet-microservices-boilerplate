using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.Enums;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs;

public class FilterAndPagingDealsDto : FilterAndPagingDto<DealSortProperty>
{
    public DealStatus? Status { get; set; }
}