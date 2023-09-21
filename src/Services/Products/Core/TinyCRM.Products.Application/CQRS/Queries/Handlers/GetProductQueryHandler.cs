using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Application.CQRS.Queries.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Exceptions;
using TinyCRM.Products.Domain.Specifications;

namespace TinyCRM.Products.Application.CQRS.Queries.Handlers;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Product> _repository;

    public GetProductQueryHandler(
        IReadOnlyRepository<Product> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productIdSpecification = new ProductIdSpecification(request.Id);

        var product = Optional<Product>.Of(await _repository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotPresent(new ProductNotFoundException(request.Id)).Get();

        return _mapper.Map<ProductDto>(product);
    }
}