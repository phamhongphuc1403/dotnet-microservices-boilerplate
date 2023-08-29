using AutoMapper;
using TinyCRM.Core.CQRS;
using TinyCRM.Core.Utils;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Application.Queries.Requests;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Exceptions;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.Application.Queries.Handlers;

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