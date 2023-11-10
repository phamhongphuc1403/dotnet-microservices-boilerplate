using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Repositories;
using ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;
using ProductManagement.Core.Application.DTOs;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Core.Domain.ProductAggregate.Specifications;

namespace ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Handlers;

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
        var productCodePartialMatchSpecification = new ProductCodePartialMatchSpecification(query.Dto.Keyword);

        var productNamePartialMatchSpecification = new ProductNamePartialMatchSpecification(query.Dto.Keyword);

        var productTypeSpecification = new ProductTypeSpecification(query.Dto.Type);

        var productKeywordPartialMatchSpecification =
            productNamePartialMatchSpecification.Or(productCodePartialMatchSpecification);

        var specification = productKeywordPartialMatchSpecification.And(productTypeSpecification);

        var (products, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Dto.Sort, query.Dto.PageIndex, query.Dto.PageSize);

        return new FilterAndPagingResultDto<ProductSummaryDto>(_mapper.Map<List<ProductSummaryDto>>(products),
            query.Dto.PageIndex, query.Dto.PageSize, totalCount);
    }
}