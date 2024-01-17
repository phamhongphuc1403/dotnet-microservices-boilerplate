using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Repositories;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
    Task<bool> CheckPasswordAsync(User user, string password);

    Task<string> GetPasswordResetToken(User user);
}