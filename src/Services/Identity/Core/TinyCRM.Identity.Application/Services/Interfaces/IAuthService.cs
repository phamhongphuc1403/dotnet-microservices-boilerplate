using System.Security.Claims;
using TinyCRM.Identities.Domain.Entities;

namespace TinyCRM.Identity.Application.Services.Interfaces;

public interface IAuthService
{
    Task<IEnumerable<Claim>> Login(string email, string password);
    Task<IEnumerable<Claim>> GetClaimsAsync(User user);
}