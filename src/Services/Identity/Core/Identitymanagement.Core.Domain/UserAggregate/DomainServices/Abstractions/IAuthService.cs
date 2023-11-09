using System.Security.Claims;
using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;

public interface IAuthService
{
    Task<User> Login(string email, string password);

    Task<IEnumerable<Claim>> GetClaimsAsync(User user);

    Task ChangePasswordAsync(User user, string currentPassword, string newPassword, string confirmPassword);
}