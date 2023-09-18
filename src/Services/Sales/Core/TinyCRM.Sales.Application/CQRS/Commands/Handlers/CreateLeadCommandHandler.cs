using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Application.CQRS.Commands.Requests;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Application.CQRS.Commands.Handlers;

public class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, LeadDto>
{
    private readonly IOperationRepository<Lead> _operationRepository;
    private readonly IReadOnlyRepository<Lead> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLeadCommandHandler(IOperationRepository<Lead> operationRepository,
        IReadOnlyRepository<Lead> readonlyRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public Task<LeadDto> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}