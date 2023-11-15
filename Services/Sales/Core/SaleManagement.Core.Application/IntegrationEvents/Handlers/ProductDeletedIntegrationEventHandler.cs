using BuildingBlock.Core.Application.IntegrationEvents.Handlers;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using SaleManagement.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Core.Application.IntegrationEvents.Handlers;

public class ProductDeletedIntegrationEventHandler : IIntegrationEventHandler<ProductDeletedIntegrationEvent>
{
    private readonly IProductDomainService _productDomainService;
    private readonly IOperationRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductDeletedIntegrationEventHandler(IOperationRepository<Product> productRepository,
        IProductDomainService productDomainService, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(ProductDeletedIntegrationEvent @event)
    {
        var product = await _productDomainService.DeleteAsync(@event.ProductId, @event.DeletedAt, @event.DeletedBy);

        _productRepository.Delete(product);

        await _unitOfWork.SaveChangesAsync();
    }
}