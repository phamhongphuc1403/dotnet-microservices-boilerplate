using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.Domain.ProductAggregate.Specifications;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Handlers;

public class
    FilterAndPagingProductsQueryHandler : IQueryHandler<FilterAndPagingProductsQuery,
        FilterAndPagingResultDto<ProductSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Product> _repository;

    public FilterAndPagingProductsQueryHandler(
        IReadOnlyRepository<Product> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<ProductSummaryDto>> Handle(FilterAndPagingProductsQuery query,
        CancellationToken cancellationToken)
    {
        var productCodePartialMatchSpecification = new ProductCodePartialMatchSpecification(query.Keyword);

        var productNamePartialMatchSpecification = new ProductNamePartialMatchSpecification(query.Keyword);

        var productTypeSpecification = new ProductTypeSpecification(query.Type);

        var productKeywordPartialMatchSpecification =
            productNamePartialMatchSpecification.Or(productCodePartialMatchSpecification);

        var specification = productKeywordPartialMatchSpecification.And(productTypeSpecification);

        var (products, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Sort, query.PageIndex, query.PageSize);

        return new FilterAndPagingResultDto<ProductSummaryDto>(_mapper.Map<List<ProductSummaryDto>>(products),
            query.PageIndex,
            query.PageSize,
            totalCount);
    }
}