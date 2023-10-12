using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Requests;

public class CreateRoleCommand : CreateOrEditRoleDto, ICommand<RoleDto>
{
    public CreateRoleCommand(CreateOrEditRoleDto dto)
    {
        Name = dto.Name;
    }
}