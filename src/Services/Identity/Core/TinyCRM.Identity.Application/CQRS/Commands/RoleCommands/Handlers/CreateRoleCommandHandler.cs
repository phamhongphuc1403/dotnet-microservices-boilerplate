using AutoMapper;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Handlers;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IRoleDomainService roleDomainService, IMapper mapper, IUnitOfWork unitOfWork,
        IRoleOperationRepository roleOperationRepository)
    {
        _roleDomainService = roleDomainService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleOperationRepository = roleOperationRepository;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleDomainService.CreateAsync(request.Name);

        await _roleOperationRepository.CreateAsync(role);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<RoleDto>(role);
    }
}