using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Utils;
using BuildingBlock.Domain.Specifications.Implementations;
using TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.Domain.ProductAggregate.Exceptions;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Handlers;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Product> _repository;

    public GetProductQueryHandler(IReadOnlyRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDetailDto> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var productIdSpecification = new EntityIdSpecification<Product>(query.ProductId);

        var product = Optional<Product>.Of(await _repository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotPresent(new ProductNotFoundException(query.ProductId)).Get();

        return _mapper.Map<ProductDetailDto>(product);
    }
}