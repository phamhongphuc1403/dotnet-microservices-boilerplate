using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;

public interface IUserDomainService
{
    void AddRefreshToken(User user, string refreshToken);

    Task<User> CreateAsync(string email, string name);

    Task DeleteAsync(User user);
}