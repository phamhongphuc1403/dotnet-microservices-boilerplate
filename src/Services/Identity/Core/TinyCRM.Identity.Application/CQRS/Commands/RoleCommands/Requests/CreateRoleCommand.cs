using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Requests;

public record CreateRoleCommand(CreateOrEditRoleDto Dto) : ICommand<RoleDto>;