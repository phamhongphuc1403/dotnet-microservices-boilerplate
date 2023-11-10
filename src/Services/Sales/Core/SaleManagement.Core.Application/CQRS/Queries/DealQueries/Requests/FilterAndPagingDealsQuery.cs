using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using SaleManagement.Core.Application.DTOs.DealDTO;

namespace SaleManagement.Core.Application.CQRS.Queries.DealQueries.Requests;

public record FilterAndPagingDealsQuery(FilterAndPagingDealsDto Dto) : IQuery<FilterAndPagingResultDto<DealDto>>
{
}