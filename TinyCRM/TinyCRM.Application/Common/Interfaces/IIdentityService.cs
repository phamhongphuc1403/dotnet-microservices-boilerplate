using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<UserEntity> GetByIdAsync(string id);

        Task<string> CreateAsync(UserEntity user);

        Task UpdateAsync(UserEntity user);
        
        Task<(List<UserEntity>, int)> GetAllAsync(UserQueryDto query);
    }
}