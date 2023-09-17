using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Application.Queries.Requests;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Exceptions;
using TinyCRM.Products.Domain.Repositories;

namespace TinyCRM.Products.Application.Queries.Handlers;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IProductReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetProductQueryHandler(
        IProductReadOnlyRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = Optional<Product>.Of(await _repository.GetByIdAsync(request.Id))
            .ThrowIfNotPresent(new ProductNotFoundException(request.Id)).Get();

        return _mapper.Map<ProductDto>(product);
    }
}