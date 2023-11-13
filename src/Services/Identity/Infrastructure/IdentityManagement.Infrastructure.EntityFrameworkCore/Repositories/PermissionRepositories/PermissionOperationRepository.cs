using System.Security.Claims;
using AutoMapper;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.Identity.Exceptions;
using Identitymanagement.Core.Domain.PermissionAggregate.Entities;
using Identitymanagement.Core.Domain.PermissionAggregate.Repositories;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;
using Identitymanagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.PermissionRepositories;

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
            .ThrowIfNotExist(new RoleNotFoundException(role.Name)).Get();
    }
}