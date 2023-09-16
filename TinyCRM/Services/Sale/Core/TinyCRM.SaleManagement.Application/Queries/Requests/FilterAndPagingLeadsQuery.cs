using BuildingBlock.Core;
using BuildingBlock.Core.CQRS;
using BuildingBlock.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.Application.Queries.Requests;

public class FilterAndPagingLeadsQuery: FilterAndPagingQuery<Lead>, IQuery<FilterAndPagingResultDto<LeadDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto) : base(dto)
    {
    }
}