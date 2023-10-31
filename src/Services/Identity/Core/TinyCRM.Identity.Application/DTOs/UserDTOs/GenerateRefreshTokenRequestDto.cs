namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class GenerateRefreshTokenRequestDto
{
    public GenerateRefreshTokenRequestDto()
    {
    }

    protected GenerateRefreshTokenRequestDto(GenerateRefreshTokenRequestDto dto)
    {
        RefreshToken = dto.RefreshToken;
    }

    public string RefreshToken { get; set; } = null!;
}