using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.User.DTOs;

namespace TinyCRM.Application.Modules.User.Services.Interfaces;

public interface IUserService
{
    Task<GetUserDto> CreateAsync(CreateOrEditUserDto dto);

    Task<GetUserDto> GetByIdAsync(string id);

    Task<GetUserDto> UpdateAsync(string id, CreateOrEditUserDto dto);

    Task<PaginationResponseDto<GetUserDto>> GetAllAsync(UserQueryDto query);
}