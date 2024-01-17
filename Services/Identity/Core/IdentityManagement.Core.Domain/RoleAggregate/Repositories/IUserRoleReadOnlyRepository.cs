using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Repositories;

public interface IUserRoleReadOnlyRepository : IReadOnlyRepository<UserRole>
{
}