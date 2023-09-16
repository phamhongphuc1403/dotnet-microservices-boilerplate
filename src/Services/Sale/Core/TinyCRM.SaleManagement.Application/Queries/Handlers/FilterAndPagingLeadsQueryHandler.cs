using AutoMapper;
using BuildingBlock.Core.CQRS;
using BuildingBlock.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Application.Queries.Requests;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.Application.Queries.Handlers;

public class FilterAndPagingLeadsQueryHandler : IQueryHandler<FilterAndPagingLeadsQuery, FilterAndPagingResultDto<LeadDto>>
{
    private readonly ILeadReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public FilterAndPagingLeadsQueryHandler(ILeadReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<FilterAndPagingResultDto<LeadDto>> Handle(FilterAndPagingLeadsQuery query,
        CancellationToken cancellationToken)
    {
        var (deals, totalCount) = await _repository.GetPagedLeadsAsync(query);

        return new FilterAndPagingResultDto<LeadDto>(_mapper.Map<List<LeadDto>>(deals), query.Page,
            query.Take, totalCount);
    }
}