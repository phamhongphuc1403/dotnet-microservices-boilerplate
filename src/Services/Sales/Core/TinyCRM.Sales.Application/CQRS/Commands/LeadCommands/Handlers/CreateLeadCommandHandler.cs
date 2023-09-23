using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Application.CQRS.Commands.LeadCommands.Requests;
using TinyCRM.Sales.Application.DTOs.LeadDTOs;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, LeadDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Lead> _operationRepository;
    private readonly IReadOnlyRepository<Lead> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

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