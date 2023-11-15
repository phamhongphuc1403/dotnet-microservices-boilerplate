using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Repositories;

public interface IUserReadOnlyRepository
{
    Task<User?> GetByIdAsync(Guid id, string? includeTables = null);

    Task<User?> GetByEmailAsync(string email, string? includeTables = null);

    Task<(IEnumerable<User> users, int totalCount)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize);

    Task<bool> CheckIfEmailExistAsync(string email);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<string> GetPasswordResetToken(User user);
}