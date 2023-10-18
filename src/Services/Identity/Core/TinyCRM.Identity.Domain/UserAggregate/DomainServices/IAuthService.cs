using System.Security.Claims;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.DomainServices;

public interface IAuthService
{
    Task<User> Login(string email, string password);

    Task<IEnumerable<Claim>> GetClaimsAsync(User user);

    Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}