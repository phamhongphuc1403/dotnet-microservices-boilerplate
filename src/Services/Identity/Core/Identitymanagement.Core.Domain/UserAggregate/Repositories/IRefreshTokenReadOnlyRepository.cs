using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.UserAggregate.Repositories;

public interface IRefreshTokenReadOnlyRepository
{
    Task<IEnumerable<RefreshToken>> GetByUserId(Guid userId);
}