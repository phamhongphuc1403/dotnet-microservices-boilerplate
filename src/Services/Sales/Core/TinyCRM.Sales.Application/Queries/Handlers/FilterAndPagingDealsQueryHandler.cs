using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Application.Queries.Requests;
using TinyCRM.Sales.Domain.Repositories;

namespace TinyCRM.Sales.Application.Queries.Handlers;

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