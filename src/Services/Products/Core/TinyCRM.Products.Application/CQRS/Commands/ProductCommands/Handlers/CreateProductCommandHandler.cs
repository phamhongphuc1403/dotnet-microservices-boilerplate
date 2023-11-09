using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.DomainServices;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Handlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductDomainService productService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(request.Dto.Code, request.Dto.Name, request.Dto.Price,
            request.Dto.IsAvailable, request.Dto.Type);

        await _operationRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDetailDto>(product);
    }
}