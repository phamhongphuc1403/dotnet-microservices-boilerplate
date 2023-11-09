using System.Security.Claims;
using AutoMapper;
using BuildingBlock.Domain.Shared.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Exceptions;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.Repositories.PermissionRepositories;

public class PermissionOperationRepository : IPermissionOperationRepository
{
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public PermissionOperationRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task AddRoleAsync(Permission permission, Role role)
    {
        var applicationRole = await GetApplicationRoleAsync(role);

        var result =
            await _roleManager.AddClaimAsync(applicationRole, new Claim(permission.Name, permission.Description));

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    private async Task<ApplicationRole> GetApplicationRoleAsync(Role role)
    {
        return Optional<ApplicationRole>.Of(await _roleManager.FindByNameAsync(role.Name))
            .ThrowIfNotPresent(new RoleNotFoundException(role.Name)).Get();
    }
}