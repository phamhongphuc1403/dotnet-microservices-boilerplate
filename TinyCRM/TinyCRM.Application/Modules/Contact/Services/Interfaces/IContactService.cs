using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Contact.DTOs;

namespace TinyCRM.Application.Modules.Contact.Services.Interfaces;

public interface IContactService
{
    Task<PaginationResponseDto<GetContactDto>> GetAllAsync(ContactQueryDto query);

    Task<GetContactDto> GetByIdAsync(Guid id);

    Task<GetContactDto> AddAsync(AddOrUpdateContactDto dto);

    Task<GetContactDto> UpdateAsync(AddOrUpdateContactDto dto, Guid id);

    Task DeleteAsync(Guid id);

    Task<PaginationResponseDto<GetContactDto>> GetAllByAccountIdAsync(Guid id, ContactQueryDto query);
}