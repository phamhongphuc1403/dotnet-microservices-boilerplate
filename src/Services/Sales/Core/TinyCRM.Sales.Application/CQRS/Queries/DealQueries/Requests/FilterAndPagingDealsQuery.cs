using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.DealDTO;

namespace TinyCRM.Sales.Application.CQRS.Queries.DealQueries.Requests;

public class FilterAndPagingDealsQuery : FilterAndPagingDealsDto, IQuery<FilterAndPagingResultDto<DealDto>>
{
    public FilterAndPagingDealsQuery(FilterAndPagingDealsDto dto) : base(dto)
    {
        Sort = dto.ConvertSort();
    }

    public string Sort { get; private init; }
}