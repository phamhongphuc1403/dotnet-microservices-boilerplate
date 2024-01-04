using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Roles.DTOs;

namespace IdentityManagement.Core.Application.Roles.CQRS.Commands.Requests;

public record CreateRoleCommand(CreateOrEditRoleDto Dto) : ICommand<RoleDto>;