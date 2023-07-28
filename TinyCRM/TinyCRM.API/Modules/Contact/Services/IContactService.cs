using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Contact.Services
{
    public interface IContactService
    {
        Task<PaginationResponse<GetContactDTO>> GetAllAsync(ContactQueryDTO query);

        Task<GetContactDTO> GetByIdAsync(Guid id);

        Task<GetContactDTO> AddAsync(AddOrUpdateContactDTO dto);

        Task<GetContactDTO> UpdateAsync(AddOrUpdateContactDTO dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponse<GetContactDTO>> GetAllByAccountIdAsync(Guid id, ContactQueryDTO query);
    }
}