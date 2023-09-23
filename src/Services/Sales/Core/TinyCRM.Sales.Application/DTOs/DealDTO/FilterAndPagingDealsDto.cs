using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.DealDTO.Enums;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs.DealDTO;

public class FilterAndPagingDealsDto : FilterAndPagingDto<DealSortProperty>
{
    public DealStatus? Status { get; set; }
}