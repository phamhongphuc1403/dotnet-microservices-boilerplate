using System.Security.Claims;

namespace TinyCRM.Identities.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<IEnumerable<Claim>> Login(string email, string password);
}