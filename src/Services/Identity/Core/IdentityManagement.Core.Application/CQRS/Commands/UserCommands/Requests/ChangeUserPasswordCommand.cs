using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.DTOs.UserDTOs;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;

public record ChangeUserPasswordCommand(Guid UserId, ChangeUserPasswordDto Dto) : ICommand;