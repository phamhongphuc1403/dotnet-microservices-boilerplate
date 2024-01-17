using System.Collections.Immutable;
using System.Security.Claims;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Permissions.Seeders;

public class PermissionSeeder : IDataSeeder
{
    private readonly ILogger<PermissionSeeder> _logger;
    private readonly IPermissionDomainService _permissionDomainService;
    private readonly IPermissionOperationRepository _permissionOperationRepository;
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionSeeder(IPermissionDomainService permissionDomainService,
        IPermissionReadOnlyRepository permissionReadOnlyRepository, IRoleReadOnlyRepository roleReadOnlyRepository,
        ILogger<PermissionSeeder> logger, IUnitOfWork unitOfWork,
        IPermissionOperationRepository permissionOperationRepository)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _permissionOperationRepository = permissionOperationRepository;
        _permissionDomainService = permissionDomainService;
    }

    public int ExecutionOrder => 2;

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding permissions");

        await SeedPermissionAsync("admin", BuildingBlock.Core.Domain.Shared.Constants.Permissions.AdminPermissions);

        await SeedPermissionAsync("user", BuildingBlock.Core.Domain.Shared.Constants.Permissions.UserPermissions);

        _logger.LogInformation("Seed permission successfully");
    }

    private async Task SeedPermissionAsync(string roleName, ImmutableList<Claim> permissions)
    {
        var currentRolePermissions = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(roleName);

        if (currentRolePermissions.Any()) return;

        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification(roleName);

        var role = Optional<Role>.Of(await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification))
            .ThrowIfNotExist(new RoleNotFoundException(roleName)).Get();

        foreach (var permission in permissions.Select(permission => new Permission(permission.Type, permission.Value)))
        {
            await _permissionDomainService.CheckValidOnAddRoleAsync(permission, role);

            await _permissionOperationRepository.AddRoleAsync(permission, role);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}