using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Products.Application.CQRS.Commands.Requests;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.Application.CQRS.Commands.Handlers;

public class CreateProductCommandHandler : IQueryHandler<CreateProductCommand, ProductDto>
{
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IReadOnlyRepository<Product> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

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