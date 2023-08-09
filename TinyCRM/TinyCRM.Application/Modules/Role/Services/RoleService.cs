using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.Role.DTOs;
using TinyCRM.Application.Modules.Role.Services.Interfaces;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Role.Services
{
    public class RoleService : IRoleService
    {
        private readonly IIdentityRoleService _identityRoleService;

        public RoleService(IIdentityRoleService identityRoleService)
        {
            _identityRoleService = identityRoleService;
        }

        public Task<List<RoleEntity>> GetAllAsync()
        {
            return _identityRoleService.GetAllRoles();
        }

        public Task<RoleEntity> GetUserRoleAsync(string userId)
        {
            return _identityRoleService.GetRoleByUserId(userId);
        }

        public Task UpdateUserRoleAsync(string userId, UpdateUserRoleDto model)
        {
            throw new NotImplementedException();
        }
    }
}
