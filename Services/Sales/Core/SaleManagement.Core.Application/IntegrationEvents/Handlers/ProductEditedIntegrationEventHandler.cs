using BuildingBlock.Core.Application.IntegrationEvents.Handlers;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using SaleManagement.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Core.Application.IntegrationEvents.Handlers;

public class ProductEditedIntegrationEventHandler : IIntegrationEventHandler<ProductEditedIntegrationEvent>
{
    private readonly IProductDomainService _productDomainService;
    private readonly IOperationRepository<Product> _productOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductEditedIntegrationEventHandler(IOperationRepository<Product> productOperationRepository,
        IProductDomainService productDomainService, IUnitOfWork unitOfWork)
    {
        _productOperationRepository = productOperationRepository;
        _productDomainService = productDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(ProductEditedIntegrationEvent @event)
    {
        var product = await _productDomainService.EditAsync(@event.ProductId, @event.ProductCode, @event.ProductName,
            @event.ProductPrice, @event.ProductIsAvailable, @event.ProductType, @event.UpdatedAt, @event.UpdatedBy);

        _productOperationRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();
    }
}