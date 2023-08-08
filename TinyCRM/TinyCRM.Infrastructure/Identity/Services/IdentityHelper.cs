using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Identity.Entities;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;

namespace TinyCRM.Infrastructure.Identity.Services
{
    public class IdentityHelper : IIdentityHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            return Optional<ApplicationUser>.Of(await _userManager.FindByIdAsync(userId))
                .ThrowIfNotPresent(new NotFoundException("User not found")).Get();
        }
    }
}