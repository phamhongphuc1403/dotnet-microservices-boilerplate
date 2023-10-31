using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.LeadDTOs;

namespace TinyCRM.Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class FilterAndPagingLeadsQuery : FilterAndPagingLeadsDto, IQuery<FilterAndPagingResultDto<LeadDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto) : base(dto)
    {
        Sort = dto.ConvertSort();
    }

    public string Sort { get; private init; }
}