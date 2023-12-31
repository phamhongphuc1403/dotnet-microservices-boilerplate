using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.Roles.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Roles.DTOs;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;

namespace IdentityManagement.Core.Application.Roles.CQRS.Commands.Handlers;

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
        var role = await _roleDomainService.CreateAsync(request.Dto.Name);

        await _roleOperationRepository.CreateAsync(role);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<RoleDto>(role);
    }
}