using AutoMapper;
using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRoleDomainService _roleDomainService;

    public CreateRoleCommandHandler(IRoleDomainService roleDomainService, IMapper mapper)
    {
        _roleDomainService = roleDomainService;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleDomainService.CreateAsync(request.Name);

        return _mapper.Map<RoleDto>(role);
    }
}