using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Account.Services
{
    public interface IAccountService
    {
        Task<PaginationResponse<GetAccountDto>> GetAllAsync(AccountQueryDto query);

        Task<GetAccountDto> GetByIdAsync(Guid id);

        Task<GetAccountDto> AddAsync(AddOrUpdateAccountDto dto);

        Task<GetAccountDto> UpdateAsync(AddOrUpdateAccountDto dto, Guid id);

        Task DeleteAsync(Guid id);
    }
}