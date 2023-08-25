using AutoMapper;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.Application.Queries;

public class
    FilterAndPagingProductQueryHandler : IQueryHandler<FilterAndPagingProductQuery,
        FilterAndPagingResultDto<GetProductDto>>
{
    private readonly IProductReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public FilterAndPagingProductQueryHandler(
        IProductReadOnlyRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<GetProductDto>> Handle(FilterAndPagingProductQuery query,
        CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _repository.GetPagedProductsAsync(query);

        return new FilterAndPagingResultDto<GetProductDto>(_mapper.Map<List<GetProductDto>>(products), query.Page,
            query.Take, totalCount);
    }
}