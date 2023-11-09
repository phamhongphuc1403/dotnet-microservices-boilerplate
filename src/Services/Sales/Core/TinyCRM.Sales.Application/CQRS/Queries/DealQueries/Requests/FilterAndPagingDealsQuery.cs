using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.DealDTO;

namespace TinyCRM.Sales.Application.CQRS.Queries.DealQueries.Requests;

public record FilterAndPagingDealsQuery(FilterAndPagingDealsDto Dto) : IQuery<FilterAndPagingResultDto<DealDto>>
{
}