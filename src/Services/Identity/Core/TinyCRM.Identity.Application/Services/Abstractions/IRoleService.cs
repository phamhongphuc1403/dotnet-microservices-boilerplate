using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface IRoleService
{
    Task<IEnumerable<string>> GetRolesAsync(User user);
}