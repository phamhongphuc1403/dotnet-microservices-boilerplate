using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

public record CreateUserCommand(User User, string? Password = null) : ICommand<User>;