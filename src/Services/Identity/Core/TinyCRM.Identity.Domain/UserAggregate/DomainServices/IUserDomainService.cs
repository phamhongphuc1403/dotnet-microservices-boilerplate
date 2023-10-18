using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.DomainServices;

public interface IUserDomainService
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(Guid id);

    Task AddRefreshTokenAsync(User user, string refreshToken);

    Task<User> RevokeRefreshToken(Guid userId, string refreshToken);

    public Task<(IEnumerable<User>, int)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize);

    Task<User> CreateAsync(string email, string password);
}