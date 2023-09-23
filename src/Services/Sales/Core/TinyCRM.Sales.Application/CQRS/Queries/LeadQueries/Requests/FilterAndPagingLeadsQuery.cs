using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.LeadDTOs;

namespace TinyCRM.Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class FilterAndPagingLeadsQuery : FilterAndPagingLeadsDto, IQuery<FilterAndPagingResultDto<LeadDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto)
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