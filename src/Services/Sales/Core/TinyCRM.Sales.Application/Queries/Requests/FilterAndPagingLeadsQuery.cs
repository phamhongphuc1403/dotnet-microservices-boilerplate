using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Application.Queries.Requests;

public class FilterAndPagingLeadsQuery: FilterAndPagingQuery<Lead>, IQuery<FilterAndPagingResultDto<LeadDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto) : base(dto)
    {
    }
}