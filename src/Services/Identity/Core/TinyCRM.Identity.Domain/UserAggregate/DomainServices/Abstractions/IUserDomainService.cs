using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;

public interface IUserDomainService
{
    void AddRefreshToken(User user, string refreshToken);

    Task<User> CreateAsync(string email, string password, string confirmPassword);

    Task<string> ResetPasswordAsync(User user, string password, string confirmPassword);
}