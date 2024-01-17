using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Roles.Seeders;

public class UserRoleSeeder : IDataSeeder
{
    private readonly ILogger<UserRoleSeeder> _logger;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserRoleReadOnlyRepository _userRoleReadOnlyRepository;

    public UserRoleSeeder(IRoleReadOnlyRepository roleReadOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository1, IUserRoleReadOnlyRepository userRoleReadOnlyRepository,
        IRoleDomainService roleDomainService, ILogger<UserRoleSeeder> logger, IUnitOfWork unitOfWork,
        IRoleOperationRepository roleOperationRepository)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository1;
        _userRoleReadOnlyRepository = userRoleReadOnlyRepository;
        _roleDomainService = roleDomainService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _roleOperationRepository = roleOperationRepository;
    }

    public int ExecutionOrder => 2;

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding user roles");

        await SeedUserRoleAsync("admin@123", "admin");

        await SeedUserRoleAsync("user@123", "user");

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed user roles successfully");
    }

    private async Task SeedUserRoleAsync(string email, string roleName)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        var user = Optional<User>
            .Of(await _userReadOnlyRepository.GetAnyAsync(userEmailExactMatchSpecification, null, true))
            .ThrowIfNotExist(new UserNotFoundException("email", email)).Get();

        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification(roleName);

        var userRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification))
            .ThrowIfNotExist(new RoleNotFoundException(roleName)).Get();

        var userRoleUserIdSpecification = new UserRoleUserIdSpecification(user.Id);

        var userRoleRoleIdSpecification = new UserRoleRoleIdSpecification(userRole.Id);

        var specification = userRoleUserIdSpecification.And(userRoleRoleIdSpecification);

        if (await _userRoleReadOnlyRepository.CheckIfExistAsync(specification)) return;

        await _roleDomainService.AddUserAsync(userRole, user);

        await _roleOperationRepository.UpdateAsync(userRole);
    }
}