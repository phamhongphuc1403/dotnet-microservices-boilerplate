using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using SaleManagement.Core.Application.CQRS.Commands.LeadCommands.Requests;
using SaleManagement.Core.Application.DTOs.LeadDTOs;
using SaleManagement.Core.Domain.LeadAggregate.Entities;

namespace SaleManagement.Core.Application.CQRS.Commands.LeadCommands.Handlers;

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