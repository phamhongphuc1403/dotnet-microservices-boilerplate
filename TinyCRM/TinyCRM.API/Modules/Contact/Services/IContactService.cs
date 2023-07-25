using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Contact.Services
{
    public interface IContactService
    {
        Task<PaginationResponse<GetContactDto>> GetAllAsync(ContactQueryDTO query);

        Task<GetContactDto> GetByIdAsync(Guid id);

        Task<GetContactDto> AddAsync(AddOrUpdateContactDto dto);

        Task<GetContactDto> UpdateAsync(AddOrUpdateContactDto dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponse<GetContactDto>> GetAllByAccountIdAsync(Guid id, ContactQueryDTO query);
    }
}