using TinyCRM.Application.Modules.Auth.DTOs;

namespace TinyCRM.Application.Modules.Auth.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto dto);

    Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
}