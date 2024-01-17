using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Repositories;

public interface IRefreshTokenReadOnlyRepository : IReadOnlyRepository<RefreshToken>
{
}