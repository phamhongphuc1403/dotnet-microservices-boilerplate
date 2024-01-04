using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Application.Permissions.DTOs;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Specifications;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.PermissionRepositories;

public class PermissionReadOnlyRepository : IPermissionReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationPermission> _permissionReadOnlyRepository;

    public PermissionReadOnlyRepository(IReadOnlyRepository<ApplicationPermission> permissionReadOnlyRepository,
        IMapper mapper)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName)
    {
        var permissionRoleNameSpecification = new PermissionRoleNameSpecification(roleName);

        var permissionNames =
            await _permissionReadOnlyRepository.GetAllAsync<PermissionNameDto>(permissionRoleNameSpecification);

        var roles = permissionNames.Select(permissionName => permissionName.Name);
        return roles;
    }

    public Task<List<Permission>> GetAllByRoleNameAsync(string roleName)
    {
        var permissionRoleNameSpecification = new PermissionRoleNameSpecification(roleName);

        return _permissionReadOnlyRepository.GetAllAsync<Permission>(permissionRoleNameSpecification);
    }
}