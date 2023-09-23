using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.DealDTO;

namespace TinyCRM.Sales.Application.CQRS.Queries.DealQueries.Requests;

public class FilterAndPagingDealsQuery : FilterAndPagingDealsDto, IQuery<FilterAndPagingResultDto<DealDto>>
{
    public FilterAndPagingDealsQuery(FilterAndPagingDealsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        Status = dto.Status;
    }

    public string Sort { get; private init; }
}