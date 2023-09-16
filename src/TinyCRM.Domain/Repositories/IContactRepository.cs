using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories;

public interface IContactRepository : IRepository<ContactEntity>
{
    Task<(List<ContactEntity>, int)> GetPagedContactsAsync(DataQueryDto<ContactEntity> query);

    Task<bool> CheckIfEmailExistAsync(string email);

    Task<bool> CheckIfEmailExistAsync(string email, Guid contactId);

    Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber);

    Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber, Guid contactId);

    Task<(List<ContactEntity>, int)> GetPagedContactsByAccountIdAsync(DataQueryDto<ContactEntity> query,
        Guid accountId);
}