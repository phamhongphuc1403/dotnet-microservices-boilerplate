using System.Security.Claims;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;

public interface IAuthService
{
    Task<User> Login(string email, string password);

    Task<IEnumerable<Claim>> GetClaimsAsync(User user);

    Task ChangePasswordAsync(User user, string currentPassword, string newPassword, string confirmPassword);
}