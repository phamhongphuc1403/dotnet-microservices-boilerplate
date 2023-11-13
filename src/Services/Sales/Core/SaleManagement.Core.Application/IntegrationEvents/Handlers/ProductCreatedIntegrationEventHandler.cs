using BuildingBlock.Core.Application.IntegrationEvents.Handlers;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using SaleManagement.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Core.Application.IntegrationEvents.Handlers;

public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    private readonly IProductDomainService _productDomainService;
    private readonly IOperationRepository<Product> _productOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCreatedIntegrationEventHandler(IOperationRepository<Product> productOperationRepository,
        IUnitOfWork unitOfWork, IProductDomainService productDomainService)
    {
        _productOperationRepository = productOperationRepository;
        _unitOfWork = unitOfWork;
        _productDomainService = productDomainService;
    }

    public async Task HandleAsync(ProductCreatedIntegrationEvent @event)
    {
        var product = await _productDomainService.CreateAsync(@event.ProductId, @event.ProductCode, @event.ProductName,
            @event.ProductPrice, @event.ProductIsAvailable, @event.ProductType, @event.CreatedAt, @event.CreatedBy);

        await _productOperationRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();
    }
}