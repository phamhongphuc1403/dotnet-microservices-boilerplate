using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identities.Domain.UserAggregate.DomainServices;

public interface IUserDomainService
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddRefreshTokenAsync(User user, string refreshToken);
    Task<User> RevokeRefreshToken(Guid userId, string refreshToken);

    public Task<(IEnumerable<User>, int)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize);
}