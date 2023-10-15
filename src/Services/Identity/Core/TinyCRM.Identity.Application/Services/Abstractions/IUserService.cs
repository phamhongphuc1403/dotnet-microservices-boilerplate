using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface IUserService
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddRefreshTokenAsync(User user, string refreshToken);
    Task<User> RevokeRefreshToken(Guid userId, string refreshToken);

    public Task<(IEnumerable<User>, int)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize);
}