using TinyCRM.Core;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.Application.Queries.Requests;

public class FilterAndPagingDealsQuery : FilterAndPagingQuery<Deal>, IQuery<FilterAndPagingResultDto<DealDto>>
{
    public FilterAndPagingDealsQuery(FilterAndPagingDealsDto dto) : base(dto)
    {
    }
}