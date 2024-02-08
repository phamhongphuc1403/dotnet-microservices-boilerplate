using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

public record GenerateTokensCommand(User User) : ICommand<TokenResponseDto>;