using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Application.CQRS.Queries.LeadQueries.Requests;
using TinyCRM.Sales.Application.DTOs.LeadDTOs;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;
using TinyCRM.Sales.Domain.LeadAggregate.Specifications;

namespace TinyCRM.Sales.Application.CQRS.Queries.LeadQueries.Handlers;

public class
    FilterAndPagingLeadsQueryHandler : IQueryHandler<FilterAndPagingLeadsQuery, FilterAndPagingResultDto<LeadDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Lead> _repository;

    public FilterAndPagingLeadsQueryHandler(IReadOnlyRepository<Lead> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<LeadDto>> Handle(FilterAndPagingLeadsQuery query,
        CancellationToken cancellationToken)
    {
        var leadTitleSpecification = new LeadTitleSpecification(query.Keyword);

        var leadStatusSpecification = new LeadStatusSpecification(query.Status);

        var specification = leadTitleSpecification.And(leadStatusSpecification);

        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Sort, query.PageIndex, query.PageSize);

        return new FilterAndPagingResultDto<LeadDto>(_mapper.Map<List<LeadDto>>(deals), query.PageIndex, query.PageSize,
            totalCount);
    }
}