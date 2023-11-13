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

public class EditProductCommandHandler : ICommandHandler<EditProductCommand, ProductDetailDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductDomainService productDomainService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _productDomainService = productDomainService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<ProductDetailDto> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productDomainService.EditAsync(request.ProductId, request.Dto.Code, request.Dto.Name,
            request.Dto.Price, request.Dto.IsAvailable, request.Dto.Type);

        _operationRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new ProductEditedIntegrationEvent(product.Id, product.Code, product.Name, product.Price,
            product.IsAvailable, product.Type, product.UpdatedAt, product.UpdatedBy));

        return _mapper.Map<ProductDetailDto>(product);
    }
}