using TinyCRM.API.Modules.User.DTOs;

namespace TinyCRM.API.Modules.User.Services
{
    public interface IUserService
    {
        Task<GetUserDTO> CreateAsync(CreateOrEditUserDTO model);

        Task<GetUserDTO> GetByIdAsync(string id);

        Task<GetUserDTO> UpdateAsync(string id, CreateOrEditUserDTO model);
    }
}