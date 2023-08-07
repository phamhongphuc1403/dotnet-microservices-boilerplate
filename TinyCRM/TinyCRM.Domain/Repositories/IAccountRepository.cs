using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories
{
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        Task<(List<AccountEntity>, int)> GetPagedAccountsAsync(DataQueryDto<AccountEntity> query);

        Task<bool> CheckIfEmailExistAsync(string email);

        Task<bool> CheckIfEmailExistAsync(string email, Guid accountId);

        Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber);

        Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber, Guid accountId);
    }
}