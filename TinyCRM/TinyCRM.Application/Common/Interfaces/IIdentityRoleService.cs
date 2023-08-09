using System.Security.Claims;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Common.Interfaces
{
    public interface IIdentityRoleService
    {
        Task AddToRoleAsync(string userId, string role);

        Task<IList<string>> GetRolesAsync(string userId);

        Task<IList<Claim>> GetClaimsByRoleIdAsync(string roleName);

        Task<List<RoleEntity>> GetAllRoles();

        Task<RoleEntity> GetRoleByUserId(string userId);
        
        Task<RoleEntity> GetRoleById(string roleId);

        Task RemoveFromRole(string userId);
    }
}