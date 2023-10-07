using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;
using TinyCRM.Products.Domain.ProductAggregate.DomainServices;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Handlers;

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
        var products = await _productDomainService.RemoveManyAsync(request.Ids);

        _operationRepository.RemoveRange(products);

        await _unitOfWork.SaveChangesAsync();
    }
}