using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Repositories;
using SaleManagement.Core.Application.CQRS.Queries.LeadQueries.Requests;
using SaleManagement.Core.Application.DTOs.LeadDTOs;
using SaleManagement.Core.Domain.LeadAggregate.Entities;
using SaleManagement.Core.Domain.LeadAggregate.Specifications;

namespace SaleManagement.Core.Application.CQRS.Queries.LeadQueries.Handlers;

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