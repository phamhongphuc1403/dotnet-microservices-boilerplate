using TinyCRM.API.Modules.User.DTOs;

namespace TinyCRM.API.Modules.User.Services
{
    public interface IUserService
    {
        Task<GetUserDTO> CreateAsync(CreateUserDTO model);
        Task<GetUserDTO> GetByIdAsync(Guid id);
    }
}
