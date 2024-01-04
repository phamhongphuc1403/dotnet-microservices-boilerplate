using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.DTOs;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

public record ChangeUserPasswordCommand(Guid UserId, ChangeUserPasswordDto Dto) : ICommand;