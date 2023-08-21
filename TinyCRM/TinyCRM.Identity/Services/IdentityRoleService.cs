using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.Identity.Entities;
using TinyCRM.Identity.Services.Interfaces;

namespace TinyCRM.Identity.Services;

public class IdentityRoleService : IIdentityRoleService
{
    private readonly IIdentityHelper _identityHelper;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public IdentityRoleService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IIdentityHelper identityHelper,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _identityHelper = identityHelper;
        _roleManager = roleManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task AddToRoleAsync(string userId, string role)
    {
        var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.First().Description);
    }

    public async Task AddToRolesAsync(string userId, IEnumerable<string> roles)
    {
        var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

        var result = await _userManager.AddToRolesAsync(user, roles);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.First().Description);
    }

    public async Task<List<RoleEntity>> GetAllRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return _mapper.Map<List<RoleEntity>>(roles);
    }

    public async Task<IEnumerable<string>> GetRoleNamesByUserId(string userId)
    {
        var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

        var roleNames = await _userManager.GetRolesAsync(user);

        return roleNames;
    }

    public async Task<RoleEntity> GetRoleById(string roleId)
    {
        var role = Optional<ApplicationRole>.Of(await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId))
            .ThrowIfNotPresent(new NotFoundException("Role not found")).Get();

        return _mapper.Map<RoleEntity>(role);
    }

    public async Task RemoveFromRole(string userId)
    {
        var user = await _identityHelper.GetApplicationUserByIdAsync(userId);

        var currentRole = await _userManager.GetRolesAsync(user);

        var result = await _userManager.RemoveFromRolesAsync(user, currentRole);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.First().Description);
    }

    public async Task<IEnumerable<PermissionEntity>> GetAllPermissionsByRoleName(string roleName)
    {
        var role = await _identityHelper.GetApplicationRoleByNameAsync(roleName);

        var claims = await _roleManager.GetClaimsAsync(role);

        return _mapper.Map<IEnumerable<PermissionEntity>>(claims);
    }

    public async Task UpdateRolePermissionAsync(string roleName, List<PermissionEntity> permissions)
    {
        var role = await _identityHelper.GetApplicationRoleByNameAsync(roleName);

        var claims = _mapper.Map<List<IdentityRoleClaim<string>>>(permissions);

        role.Claims = claims;
        await _unitOfWork.CommitAsync();
    }
}