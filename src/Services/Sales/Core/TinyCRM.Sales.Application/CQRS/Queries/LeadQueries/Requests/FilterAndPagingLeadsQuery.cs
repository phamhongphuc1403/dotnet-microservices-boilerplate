using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using TinyCRM.Sales.Application.DTOs.LeadDTOs;

namespace TinyCRM.Sales.Application.CQRS.Queries.LeadQueries.Requests;

public record FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto Dto) : IQuery<FilterAndPagingResultDto<LeadDto>>
{
}