using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;

namespace TinyCRM.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IIdentityHelper _identityHelper;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IIdentityHelper identityHelper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _identityHelper = identityHelper;
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            var user = await _identityHelper.GetApplicationUserByIdAsync(id);

            return _mapper.Map<UserEntity>(user);
        }

        public async Task<string> CreateAsync(UserEntity user)
        {
            var newUser = _mapper.Map<ApplicationUser>(user);

            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.First().Description);
            }

            return newUser.Id;
        }

        public async Task UpdateAsync(UserEntity user)
        {
            var currentUser = await _identityHelper.GetApplicationUserByIdAsync(user.Id.ToString());

            _mapper.Map(user, currentUser);

            var result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.First().Description);
            }
        }
    }
}