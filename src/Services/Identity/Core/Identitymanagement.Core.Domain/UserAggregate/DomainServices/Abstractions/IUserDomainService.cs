using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;

public interface IUserDomainService
{
    void AddRefreshToken(User user, string refreshToken);

    Task<User> CreateAsync(string email, string password, string confirmPassword);

    Task<string> ResetPasswordAsync(User user, string password, string confirmPassword);
}