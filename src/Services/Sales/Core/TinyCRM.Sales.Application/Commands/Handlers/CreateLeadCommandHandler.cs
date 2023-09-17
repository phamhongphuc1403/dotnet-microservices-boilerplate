using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain;
using TinyCRM.Sales.Application.Commands.Requests;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Repositories;

namespace TinyCRM.Sales.Application.Commands.Handlers;

public class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, LeadDto>
{
    private readonly ILeadOperationRepository _operationRepository;
    private readonly ILeadReadOnlyRepository _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLeadCommandHandler(ILeadOperationRepository operationRepository,
        ILeadReadOnlyRepository readonlyRepository, IUnitOfWork unitOfWork, IMapper mapper)
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