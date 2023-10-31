using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Requests;

public class CreateRoleCommand : CreateOrEditRoleDto, ICommand<RoleDto>
{
    public CreateRoleCommand(CreateOrEditRoleDto dto) : base(dto)
    {
    }
}