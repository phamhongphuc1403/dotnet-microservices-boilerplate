using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Account.DTOs;

namespace TinyCRM.Application.Modules.Account.Services.Interfaces
{
    public interface IAccountService
    {
        Task<PaginationResponseDto<GetAccountDto>> GetAllAsync(AccountQueryDto query);

        Task<GetAccountDto> GetByIdAsync(Guid id);

        Task<GetAccountDto> AddAsync(AddOrUpdateAccountDto dto);

        Task<GetAccountDto> UpdateAsync(AddOrUpdateAccountDto dto, Guid id);

        Task DeleteAsync(Guid id);
    }
}