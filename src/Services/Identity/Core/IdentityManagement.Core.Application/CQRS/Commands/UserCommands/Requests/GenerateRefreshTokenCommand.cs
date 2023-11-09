using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.DTOs.UserDTOs;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;

public record GenerateRefreshTokenCommand(GenerateRefreshTokenRequestDto Dto) : ICommand<LoginResponseDto>;