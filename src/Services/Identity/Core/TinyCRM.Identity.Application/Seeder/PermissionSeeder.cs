using BuildingBlock.Application;
using BuildingBlock.Domain.Shared.Constants.Identity;
using BuildingBlock.Domain.Shared.Services;
using BuildingBlock.Domain.Shared.Utils;
using Microsoft.Extensions.Logging;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Exceptions;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;

namespace TinyCRM.Identity.Application.Seeder;

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

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding permissions");

        var adminPermission = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync("admin");

        if (adminPermission.Any()) return;

        var adminRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("admin"))
            .ThrowIfNotPresent(new RoleNotFoundException("admin")).Get();

        foreach (var newPermission in Permissions.AdminPermissions.Select(permission =>
                     new Permission(permission.Type, permission.Value)))
        {
            await _permissionDomainService.CheckValidOnAddRoleAsync(newPermission, adminRole);

            await _permissionOperationRepository.AddRoleAsync(newPermission, adminRole);

            await _unitOfWork.SaveChangesAsync();
        }

        var userPermission = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync("user");

        if (userPermission.Any()) return;

        var userRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("user"))
            .ThrowIfNotPresent(new RoleNotFoundException("user")).Get();

        foreach (var newPermission in Permissions.UserPermissions.Select(permission =>
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