using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;
using ProductManagement.Core.Application.IntegrationEvents.Events;
using ProductManagement.Core.Domain.ProductAggregate.DomainServices;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Handlers;

public class DeleteManyProductsCommandHandler : ICommandHandler<DeleteManyProductsCommand>
{
    private readonly IEventBus _eventBus;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyProductsCommandHandler(IOperationRepository<Product> operationRepository,
        IProductDomainService productDomainService, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _operationRepository = operationRepository;
        _productDomainService = productDomainService;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteManyProductsCommand request, CancellationToken cancellationToken)
    {
        var products = (await _productDomainService.DeleteManyAsync(request.Dto.Ids)).ToList();

        _operationRepository.DeleteRange(products);

        await _unitOfWork.SaveChangesAsync();

        foreach (var product in products)
            _eventBus.Publish(new ProductDeletedIntegrationEvent(product.Id, product.DeletedAt, product.DeletedBy));
    }
}