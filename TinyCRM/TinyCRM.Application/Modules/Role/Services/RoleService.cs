using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.Role.DTOs;
using TinyCRM.Application.Modules.Role.Services.Interfaces;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Application.Modules.Role.Services
{
    public class RoleService : IRoleService
    {
        private readonly IIdentityRoleService _identityRoleService;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(
            IIdentityRoleService identityRoleService,
            IIdentityService identityService,
            IUnitOfWork unitOfWork)
        {
            _identityRoleService = identityRoleService;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public Task<List<RoleEntity>> GetAllAsync()
        {
            return _identityRoleService.GetAllRoles();
        }

        public Task<RoleEntity> GetUserRoleAsync(string userId)
        {
            return _identityRoleService.GetRoleByUserId(userId);
        }

        public async Task UpdateUserRoleAsync(string userId, UpdateUserRoleDto model)
        {
            await _identityRoleService.GetRoleById(model.RoleId);
            await _identityService.GetByIdAsync(userId);

            await CheckValidOnUpdate(userId, model);
        }

        private async Task CheckValidOnUpdate(string userId, UpdateUserRoleDto model)
        {
            try
            {
                await CheckValidOnUpdate(userId, model.RoleId);

                var role = await _identityRoleService.GetRoleById(model.RoleId);

                await _unitOfWork.BeginTransactionAsync();

                await _identityRoleService.RemoveFromRole(userId);

                await _identityRoleService.AddToRoleAsync(userId, role.Name);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task CheckValidOnUpdate(string userId, string roleId)
        {
            var userRole = await _identityRoleService.GetRoleByUserId(userId);
           
            var role = await _identityRoleService.GetRoleById(roleId);

            if (userRole.Name == Domain.Constants.Role.SuperAdmin || role.Name == Domain.Constants.Role.SuperAdmin)
            {
                throw new ForbiddenException("You can not update super admin role");
            }
        }
    }
}
