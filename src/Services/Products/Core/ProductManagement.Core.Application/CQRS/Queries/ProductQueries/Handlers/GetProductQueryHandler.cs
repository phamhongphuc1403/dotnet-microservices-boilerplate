using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;
using ProductManagement.Core.Application.DTOs.ProductDTOs;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Core.Domain.ProductAggregate.Exceptions;

namespace ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Handlers;

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
            .ThrowIfNotExist(new ProductNotFoundException(query.ProductId)).Get();

        return _mapper.Map<ProductDetailDto>(product);
    }
}