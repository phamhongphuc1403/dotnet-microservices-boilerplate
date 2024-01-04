using BuildingBlock.Core.Application.CQRS;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

public record DeleteUserCommand(Guid UserId) : ICommand;