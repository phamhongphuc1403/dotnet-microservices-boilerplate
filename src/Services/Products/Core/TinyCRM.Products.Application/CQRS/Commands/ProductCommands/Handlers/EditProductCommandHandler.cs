using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.DomainServices;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Handlers;

public class EditProductCommandHandler : ICommandHandler<EditProductCommand, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductDomainService productDomainService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork)
    {
        _productDomainService = productDomainService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailDto> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productDomainService.EditAsync(request.Id, request.Code, request.Name, request.Price,
            request.IsAvailable, request.Type);

        _operationRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDetailDto>(product);
    }
}