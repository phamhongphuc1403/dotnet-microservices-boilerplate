using TinyCRM.API.Modules.Account.Model;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Contact.Services
{
    public interface IContactService
    {
        Task<IList<GetContactDTO>> GetAllAsync(int? skip, int? take, string? name, string? sortBy, bool? descending);
        Task<GetContactDTO> GetByIdAsync(Guid id);
        Task<GetContactDTO> AddAsync(AddOrUpdateContactDTO dto);
        Task<GetContactDTO> UpdateAsync(AddOrUpdateContactDTO dto, Guid id);
        Task DeleteAsync(Guid id);
        Task<IList<GetContactDTO>> GetAllByAccountIdAsync(Guid accountId, int? skip, int? take, string? name, string? sortBy, bool? descending);
    }
}
