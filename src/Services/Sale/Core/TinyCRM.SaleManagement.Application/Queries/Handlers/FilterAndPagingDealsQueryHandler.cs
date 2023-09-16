using AutoMapper;
using BuildingBlock.Core.CQRS;
using BuildingBlock.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Application.Queries.Requests;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.Application.Queries.Handlers;

public class
    FilterAndPagingDealsQueryHandler : IQueryHandler<FilterAndPagingDealsQuery, FilterAndPagingResultDto<DealDto>>
{
    private readonly IDealReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public FilterAndPagingDealsQueryHandler(IDealReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<FilterAndPagingResultDto<DealDto>> Handle(FilterAndPagingDealsQuery query,
        CancellationToken cancellationToken)
    {
        var (deals, totalCount) = await _repository.GetPagedDealsAsync(query);

        return new FilterAndPagingResultDto<DealDto>(_mapper.Map<List<DealDto>>(deals), query.Page,
            query.Take, totalCount);
    }
}