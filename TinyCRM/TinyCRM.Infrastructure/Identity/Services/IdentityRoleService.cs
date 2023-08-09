using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Identity.Entities;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;

namespace TinyCRM.Infrastructure.Identity.Services
{
    public class IdentityRoleService : IIdentityRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IIdentityHelper _identityHelper;
        private readonly IMapper _mapper;

        public IdentityRoleService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IIdentityHelper identityHelper,
            IMapper mapper)
        {
            _userManager = userManager;
            _identityHelper = identityHelper;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task AddToRoleAsync(string userId, string role)
        {
            var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.First().Description);
            }
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

            return _userManager.GetRolesAsync(user).Result;
        }

        public async Task<IList<Claim>> GetClaimsByRoleIdAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                return claims;
            }

            return new List<Claim>();
        }

        public async Task<List<RoleEntity>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return _mapper.Map<List<RoleEntity>>(roles);
        }

        public async Task<RoleEntity> GetRoleByUserId(string userId)
        {
            var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

            var roleNames = await _userManager.GetRolesAsync(user);

            var roleName = roleNames.FirstOrDefault() ?? throw new InvalidOperationException("User has no role");

            var role = await _roleManager.FindByNameAsync(roleName);

            return _mapper.Map<RoleEntity>(role);
        }
    }
}