using AutoMapper;
using BuildingBlock.Core;
using BuildingBlock.Core.CQRS;
using BuildingBlock.Core.EventBus.Interfaces;
using TinyCRM.ProductManagement.Application.Commands.Requests;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.Application.Commands.Handlers;

public class CreateProductCommandHandler : IQueryHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductOperationRepository _operationRepository;
    private readonly IProductReadOnlyRepository _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public CreateProductCommandHandler(
        IProductOperationRepository operationRepository,
        IProductReadOnlyRepository readonlyRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IEventBus eventBus
        )
    {
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await Product.CreateAsync(request.Code, request.Name, request.Price, request.IsAvailable,
            request.Type, _readonlyRepository);

        _operationRepository.Add(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }
}