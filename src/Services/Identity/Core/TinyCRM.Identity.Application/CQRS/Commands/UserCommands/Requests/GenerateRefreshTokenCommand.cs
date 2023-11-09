using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public record GenerateRefreshTokenCommand(GenerateRefreshTokenRequestDto Dto) : ICommand<LoginResponseDto>;