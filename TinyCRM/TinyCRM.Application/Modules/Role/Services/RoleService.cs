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

        public Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            return _identityRoleService.GetRoleNamesByUserId(userId);
        }

        public async Task UpdateUserRoleAsync(string userId, UpdateUserRoleDto model)
        {
            await CheckValidOnUpdate(userId, model.Name);
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _identityRoleService.RemoveFromRole(userId);

                await _identityRoleService.AddToRolesAsync(userId, model.Name);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task CheckValidOnUpdate(string userId, IEnumerable<string> roleNames)
        {
            var roles = await _identityRoleService.GetRoleNamesByUserId(userId);


            if (roleNames.Any(role => role == Domain.Constants.Role.SuperAdmin) || roles.Any(role => role == Domain.Constants.Role.SuperAdmin))
            {
                throw new ForbiddenException("You can not update super admin role");
            }
        }
    }
}
