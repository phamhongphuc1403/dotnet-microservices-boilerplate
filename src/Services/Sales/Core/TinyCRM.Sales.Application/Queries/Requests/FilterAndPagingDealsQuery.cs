using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Application.Queries.Requests;

public class FilterAndPagingDealsQuery : FilterAndPagingQuery<Deal>, IQuery<FilterAndPagingResultDto<DealDto>>
{
    public FilterAndPagingDealsQuery(FilterAndPagingDealsDto dto) : base(dto)
    {
    }
}