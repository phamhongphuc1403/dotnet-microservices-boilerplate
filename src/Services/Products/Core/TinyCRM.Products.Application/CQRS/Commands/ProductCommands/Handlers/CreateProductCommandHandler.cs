using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.EventBus.Abstractions;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Handlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IReadOnlyRepository<Product> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IOperationRepository<Product> operationRepository,
        IReadOnlyRepository<Product> readonlyRepository,
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

        await _operationRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }
}