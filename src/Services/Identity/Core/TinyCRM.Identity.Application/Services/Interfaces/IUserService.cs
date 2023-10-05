using TinyCRM.Identities.Domain.Entities;

namespace TinyCRM.Identity.Application.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetByEmailAsync(string email);
    Task<IList<string>> GetRolesAsync(User user);
    Task<User?> FindByIdAsync(string id);
}