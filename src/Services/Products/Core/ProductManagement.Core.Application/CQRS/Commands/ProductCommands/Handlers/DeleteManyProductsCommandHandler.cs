using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;
using ProductManagement.Core.Domain.ProductAggregate.DomainServices;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Handlers;

public class DeleteManyProductsCommandHandler : ICommandHandler<DeleteManyProductsCommand>
{
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyProductsCommandHandler(IOperationRepository<Product> operationRepository,
        IProductDomainService productDomainService, IUnitOfWork unitOfWork)
    {
        _operationRepository = operationRepository;
        _productDomainService = productDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteManyProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _productDomainService.RemoveManyAsync(request.Dto.Ids);

        _operationRepository.RemoveRange(products);

        await _unitOfWork.SaveChangesAsync();
    }
}