using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Repositories;
using SaleManagement.Core.Application.CQRS.Queries.DealQueries.Requests;
using SaleManagement.Core.Application.DTOs.DealDTO;
using SaleManagement.Core.Domain.DealAggregate.Entities;
using SaleManagement.Core.Domain.DealAggregate.Specifications;

namespace SaleManagement.Core.Application.CQRS.Queries.DealQueries.Handlers;

public class
    FilterAndPagingDealsQueryHandler : IQueryHandler<FilterAndPagingDealsQuery, FilterAndPagingResultDto<DealDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Deal> _repository;

    public FilterAndPagingDealsQueryHandler(IReadOnlyRepository<Deal> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<DealDto>> Handle(FilterAndPagingDealsQuery query,
        CancellationToken cancellationToken)
    {
        var leadTitleSpecification = new DealTitleSpecification(query.Dto.Keyword);

        var leadStatusSpecification = new DealStatusSpecification(query.Dto.Status);

        var specification = leadTitleSpecification.And(leadStatusSpecification);

        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Dto.Sort, query.Dto.PageIndex, query.Dto.PageSize);

        return new FilterAndPagingResultDto<DealDto>(_mapper.Map<List<DealDto>>(deals), query.Dto.PageIndex,
            query.Dto.PageSize, totalCount);
    }
}