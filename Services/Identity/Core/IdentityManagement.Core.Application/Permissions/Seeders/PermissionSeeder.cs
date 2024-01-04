using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
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

        var adminPermission = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync("admin");

        if (adminPermission.Any()) return;

        var adminRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("admin"))
            .ThrowIfNotExist(new RoleNotFoundException("admin")).Get();

        foreach (var newPermission in BuildingBlock.Core.Domain.Shared.Constants.Permissions.AdminPermissions.Select(
                     permission =>
                         new Permission(permission.Type, permission.Value)))
        {
            await _permissionDomainService.CheckValidOnAddRoleAsync(newPermission, adminRole);

            await _permissionOperationRepository.AddRoleAsync(newPermission, adminRole);

            await _unitOfWork.SaveChangesAsync();
        }

        var userPermission = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync("user");

        if (userPermission.Any()) return;

        var userRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("user"))
            .ThrowIfNotExist(new RoleNotFoundException("user")).Get();

        foreach (var newPermission in BuildingBlock.Core.Domain.Shared.Constants.Permissions.UserPermissions.Select(
                     permission =>
                         new Permission(permission.Type, permission.Value)))
        {
            await _permissionDomainService.CheckValidOnAddRoleAsync(newPermission, userRole);

            await _permissionOperationRepository.AddRoleAsync(newPermission, userRole);

            await _unitOfWork.SaveChangesAsync();
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed permission successfully");
    }
}