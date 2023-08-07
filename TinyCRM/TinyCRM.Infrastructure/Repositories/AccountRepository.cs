using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Infrastructure.Repositories
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<(List<AccountEntity>, int)> GetPagedAccountsAsync(DataQueryDto<AccountEntity> query)
        {
            return GetPaginationAsync(
                PaginationBuilder<AccountEntity>
                    .Init(query)
                    .Build());
        }

        public Task<bool> CheckIfEmailExistAsync(string email)
        {
            return CheckIfExistAsync(account => account.Email == email);
        }

        public Task<bool> CheckIfEmailExistAsync(string email, Guid accountId)
        {
            return CheckIfExistAsync(account => account.Email == email && account.Id != accountId);
        }

        public Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber)
        {
            return CheckIfExistAsync(account => account.PhoneNumber == phoneNumber);
        }

        public Task<bool> CheckIfPhoneNumberExistAsync(string phoneNumber, Guid accountId)
        {
            return CheckIfExistAsync(account => account.PhoneNumber == phoneNumber && account.Id != accountId);
        }
    }
}