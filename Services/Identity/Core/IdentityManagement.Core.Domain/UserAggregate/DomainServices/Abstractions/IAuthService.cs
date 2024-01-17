using System.Security.Claims;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;

public interface IAuthService
{
    Task<User> Login(string email, string password);

    Task<IEnumerable<Claim>> GetClaimsAsync(User user);

    Task ChangePasswordAsync(User user, string currentPassword, string newPassword);
}