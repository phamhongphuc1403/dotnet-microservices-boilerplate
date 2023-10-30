using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.Repositories;

public interface IRefreshTokenReadOnlyRepository
{
    Task<IEnumerable<RefreshToken>> GetByUserId(Guid userId);
}