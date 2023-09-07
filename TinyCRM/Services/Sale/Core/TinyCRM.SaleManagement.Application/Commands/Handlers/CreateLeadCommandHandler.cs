using AutoMapper;
using TinyCRM.Core;
using TinyCRM.Core.CQRS;
using TinyCRM.SaleManagement.Application.Commands.Requests;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.Application.Commands.Handlers;

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