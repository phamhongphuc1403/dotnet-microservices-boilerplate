using TinyCRM.Application.Modules.Auth.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Common.Interfaces
{
    public interface IIdentityAuthService
    {
        Task<UserEntity> AuthenticateUserAsync(LoginDTO dto);
        Task UpdatePasswordAsync(string userId, string password);
    }
}