using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.Account.Model;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Account.Services
{
    public interface IAccountService
    {
        Task<IList<GetAccountDTO>> GetAllAsync(int? skip, int? take, string? name, string? sortBy, bool? descending);
        Task<GetAccountDTO> GetByIdAsync(Guid id);
        Task<GetAccountDTO> AddAsync(AddOrUpdateAccountDTO dto);
        Task<GetAccountDTO> UpdateAsync(AddOrUpdateAccountDTO dto, Guid id);
        Task DeleteAsync(Guid id);
    }
}