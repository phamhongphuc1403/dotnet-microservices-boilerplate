using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Application.CQRS.Queries.DealQueries.Requests;
using TinyCRM.Sales.Application.DTOs.DealDTO;
using TinyCRM.Sales.Domain.DealAggregate.Entities;
using TinyCRM.Sales.Domain.DealAggregate.Specifications;

namespace TinyCRM.Sales.Application.CQRS.Queries.DealQueries.Handlers;

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
        var leadTitleSpecification = new DealTitleSpecification(query.Keyword);

        var leadStatusSpecification = new DealStatusSpecification(query.Status);
   
        var specification = leadTitleSpecification.And(leadStatusSpecification);

        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Sort, query.PageIndex, query.PageSize);

        return new FilterAndPagingResultDto<DealDto>(_mapper.Map<List<DealDto>>(deals), query.PageIndex, query.PageSize,
            totalCount);
    }
}