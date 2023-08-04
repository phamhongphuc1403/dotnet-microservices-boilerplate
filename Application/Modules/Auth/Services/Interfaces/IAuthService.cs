using TinyCRM.Application.Modules.Auth.DTOs;

namespace TinyCRM.Application.Modules.Auth.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO dto);

        Task<RefreshTokenResponseDTO> RefreshTokenAsync(RefreshTokenDTO dto);
    }
}