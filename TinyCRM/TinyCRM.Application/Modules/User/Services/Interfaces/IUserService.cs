using TinyCRM.Application.Modules.User.DTOs;

namespace TinyCRM.Application.Modules.User.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserDTO> CreateAsync(CreateOrEditUserDTO dto);

        Task<GetUserDTO> GetByIdAsync(string id);

        Task<GetUserDTO> UpdateAsync(string id, CreateOrEditUserDTO dto);
    }
}