using AutoMapper;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.Identity.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.RoleRepositories;

public class RoleOperationRepository : IRoleOperationRepository
{
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleOperationRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task CreateAsync(Role role)
    {
        var applicationRole = _mapper.Map<ApplicationRole>(role);

        var result = await _roleManager.CreateAsync(applicationRole);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        _mapper.Map(applicationRole, role);
    }

    public async Task UpdateAsync(Role role)
    {
        var applicationRole = await GetApplicationRoleAsync(role);

        var result = await _roleManager.UpdateAsync(applicationRole);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    private async Task<ApplicationRole> GetApplicationRoleAsync(Role role)
    {
        var applicationRole = Optional<ApplicationRole>.Of(await _roleManager.FindByNameAsync(role.Name))
            .ThrowIfNotExist(new RoleNotFoundException(role.Name)).Get();

        _mapper.Map(role, applicationRole);

        return applicationRole;
    }
}