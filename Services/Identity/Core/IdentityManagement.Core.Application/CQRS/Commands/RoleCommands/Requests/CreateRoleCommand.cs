using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.DTOs.RoleDTOs;

namespace IdentityManagement.Core.Application.CQRS.Commands.RoleCommands.Requests;

public record CreateRoleCommand(CreateOrEditRoleDto Dto) : ICommand<RoleDto>;