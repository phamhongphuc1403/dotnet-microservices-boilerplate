using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Infrastructure.Repositories;

public class ContactRepository : Repository<ContactEntity>, IContactRepository
{
    public ContactRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Task<(List<ContactEntity>, int)> GetPagedContactsAsync(DataQueryDto<ContactEntity> query)
    {
        return GetPaginationAsync(
            PaginationBuilder<ContactEntity>
                .Init(query)
                .Build());
    }

    public Task<bool> CheckIfEmailExistAsync(string email)
    {
        return CheckIfExistAsync(contact => contact.Email == email);
    }

    public Task<bool> CheckIfEmailExistAsync(string email, Guid contactId)
    {
        return CheckIfExistAsync(contact => contact.Email == email && contact.Id != contactId);
    }

    public Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber)
    {
        return CheckIfExistAsync(contact => contact.PhoneNumber == phoneNumber);
    }

    public Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber, Guid contactId)
    {
        return CheckIfExistAsync(contact => contact.PhoneNumber == phoneNumber && contact.Id != contactId);
    }

    public Task<(List<ContactEntity>, int)> GetPagedContactsByAccountIdAsync(DataQueryDto<ContactEntity> query,
        Guid accountId)
    {
        return GetPaginationAsync(
            PaginationBuilder<ContactEntity>
                .Init(query)
                .AddConstraint(entity => entity.AccountId == accountId)
                .Build());
    }
}