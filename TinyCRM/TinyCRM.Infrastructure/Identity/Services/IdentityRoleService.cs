using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;

namespace TinyCRM.Infrastructure.Identity.Services
{
    public class IdentityRoleService : IIdentityRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityHelper _identityHelper;

        public IdentityRoleService(
            UserManager<ApplicationUser> userManager,
            IIdentityHelper identityHelper)
        {
            _userManager = userManager;
            _identityHelper = identityHelper;
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
    }
}