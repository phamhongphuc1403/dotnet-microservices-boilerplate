using System.Security.Claims;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identities.Domain.UserAggregate.DomainServices;

public interface IAuthService
{
    Task<User> Login(string email, string password);

    Task<IEnumerable<Claim>> GetClaimsAsync(User user);
}