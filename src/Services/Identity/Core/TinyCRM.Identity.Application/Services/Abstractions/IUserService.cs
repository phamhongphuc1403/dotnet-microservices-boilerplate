using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface IUserService
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(string id);
}