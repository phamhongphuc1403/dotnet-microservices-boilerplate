using TinyCRM.API.Modules.Auth.DTOs;

namespace TinyCRM.API.Modules.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO model);
        Task<RefreshTokenResponseDTO> RefreshTokenAsync(RefreshTokenDTO model);
    }
}