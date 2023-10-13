using AutoMapper;
using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleService.CreateAsync(request.Name);

        return _mapper.Map<RoleDto>(role);
    }
}