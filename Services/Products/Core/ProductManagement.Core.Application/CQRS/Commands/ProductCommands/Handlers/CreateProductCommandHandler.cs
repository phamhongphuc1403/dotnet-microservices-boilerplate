using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;
using ProductManagement.Core.Application.DTOs.ProductDTOs;
using ProductManagement.Core.Application.IntegrationEvents.Events;
using ProductManagement.Core.Domain.ProductAggregate.DomainServices;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Handlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDetailDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductDomainService productService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _productService = productService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<ProductDetailDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(request.Dto.Code, request.Dto.Name, request.Dto.Price,
            request.Dto.IsAvailable, request.Dto.Type);

        await _operationRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new ProductCreatedIntegrationEvent(product.Id, product.Code, product.Name,
            product.Price, product.IsAvailable, product.Type, product.CreatedAt, product.CreatedBy));

        return _mapper.Map<ProductDetailDto>(product);
    }
}