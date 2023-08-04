using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Contact.DTOs;

namespace TinyCRM.Application.Modules.Contact.Services.Interfaces
{
    public interface IContactService
    {
        Task<PaginationResponseDTO<GetContactDTO>> GetAllAsync(ContactQueryDTO query);

        Task<GetContactDTO> GetByIdAsync(Guid id);

        Task<GetContactDTO> AddAsync(AddOrUpdateContactDTO dto);

        Task<GetContactDTO> UpdateAsync(AddOrUpdateContactDTO dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponseDTO<GetContactDTO>> GetAllByAccountIdAsync(Guid id, ContactQueryDTO query);
    }
}