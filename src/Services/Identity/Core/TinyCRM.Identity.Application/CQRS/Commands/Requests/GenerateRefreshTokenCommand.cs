using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Requests;

public class GenerateRefreshTokenCommand : GenerateRefreshTokenRequestDto, ICommand<LoginResponseDto>
{
    public GenerateRefreshTokenCommand(GenerateRefreshTokenRequestDto dto)
    {
        RefreshToken = dto.RefreshToken;
    }
}