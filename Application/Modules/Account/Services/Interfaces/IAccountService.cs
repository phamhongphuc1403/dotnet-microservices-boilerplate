using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Account.DTOs;

namespace TinyCRM.Application.Modules.Account.Services.Interfaces
{
    public interface IAccountService
    {
        Task<PaginationResponseDTO<GetAccountDTO>> GetAllAsync(AccountQueryDTO query);

        Task<GetAccountDTO> GetByIdAsync(Guid id);

        Task<GetAccountDTO> AddAsync(AddOrUpdateAccountDTO dto);

        Task<GetAccountDTO> UpdateAsync(AddOrUpdateAccountDTO dto, Guid id);

        Task DeleteAsync(Guid id);
    }
}