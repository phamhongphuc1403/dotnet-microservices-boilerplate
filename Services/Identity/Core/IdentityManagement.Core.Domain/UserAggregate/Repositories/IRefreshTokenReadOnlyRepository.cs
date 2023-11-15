using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Repositories;

public interface IRefreshTokenReadOnlyRepository
{
    Task<IEnumerable<RefreshToken>> GetByUserId(Guid userId);
}