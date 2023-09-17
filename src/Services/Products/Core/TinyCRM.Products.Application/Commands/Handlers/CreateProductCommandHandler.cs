using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain;
using TinyCRM.Products.Application.Commands.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Repositories;

namespace TinyCRM.Products.Application.Commands.Handlers;

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