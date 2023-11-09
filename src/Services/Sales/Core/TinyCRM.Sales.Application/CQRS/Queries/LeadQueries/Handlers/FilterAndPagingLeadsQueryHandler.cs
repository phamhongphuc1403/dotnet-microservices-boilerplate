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
        var leadTitleSpecification = new LeadTitleSpecification(query.Dto.Keyword);

        var leadStatusSpecification = new LeadStatusSpecification(query.Dto.Status);

        var specification = leadTitleSpecification.And(leadStatusSpecification);

        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Dto.Sort, query.Dto.PageIndex, query.Dto.PageSize);

        return new FilterAndPagingResultDto<LeadDto>(_mapper.Map<List<LeadDto>>(deals), query.Dto.PageIndex,
            query.Dto.PageSize, totalCount);
    }
}