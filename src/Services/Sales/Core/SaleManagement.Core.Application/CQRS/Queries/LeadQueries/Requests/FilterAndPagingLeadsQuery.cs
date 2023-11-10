using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.LeadDTOs;

namespace SaleManagement.Core.Application.CQRS.Queries.LeadQueries.Requests;

public record FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto Dto) : IQuery<FilterAndPagingResultDto<LeadDto>>
{
}