using AutoMapper;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Application.Queries.Requests;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.Application.Queries.Handlers;

public class
    FilterAndPagingProductsQueryHandler : IQueryHandler<FilterAndPagingProductsQuery,
        FilterAndPagingResultDto<ProductDto>>
{
    private readonly IProductReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public FilterAndPagingProductsQueryHandler(
        IProductReadOnlyRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<ProductDto>> Handle(FilterAndPagingProductsQuery query,
        CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _repository.GetPagedProductsAsync(query);

        return new FilterAndPagingResultDto<ProductDto>(_mapper.Map<List<ProductDto>>(products), query.Page,
            query.Take, totalCount);
    }
}