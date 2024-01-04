using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Repositories;

public interface IUserReadOnlyRepository
{
    Task<User?> GetByIdAsync(Guid id, string? includeTables = null);

    Task<TDto?> GetByIdAsync<TDto>(Guid id, string? includeTables = null, bool ignoreQueryFilters = false);

    Task<User?> GetByEmailAsync(string email, string? includeTables = null, bool ignoreQueryFilters = false);

    Task<TDto?> GetByEmailAsync<TDto>(string email, string? includeTables = null);

    Task<(IEnumerable<User> users, int totalCount)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize, string? includeTables = null, bool ignoreQueryFilters = false);

    Task<bool> CheckIfEmailIsExistAsync(string email);

    Task<bool> CheckIfPhoneNumberIsExistAsync(string phoneNumber);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<string> GetPasswordResetToken(User user);
}