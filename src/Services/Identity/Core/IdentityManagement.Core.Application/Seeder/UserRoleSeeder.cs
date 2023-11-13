using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;
using Identitymanagement.Core.Domain.RoleAggregate.Exceptions;
using Identitymanagement.Core.Domain.RoleAggregate.Repositories;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Seeder;

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

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding user roles");

        var admin = Optional<User>.Of(await _userReadOnlyRepository.GetByEmailAsync("admin@123"))
            .ThrowIfNotExist(new UserNotFoundException("email", "admin@123")).Get();

        var adminRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("admin"))
            .ThrowIfNotExist(new RoleNotFoundException("admin")).Get();

        if (await _userRoleReadOnlyRepository.GetByUserIdAndRoleIdAsync(admin.Id, adminRole.Id) == null)
        {
            await _roleDomainService.AddUserAsync(adminRole, admin);

            await _roleOperationRepository.UpdateAsync(adminRole);
        }

        var user = Optional<User>.Of(await _userReadOnlyRepository.GetByEmailAsync("user@123"))
            .ThrowIfNotExist(new UserNotFoundException("email", "admin@123")).Get();

        var userRole = Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync("user"))
            .ThrowIfNotExist(new RoleNotFoundException("user")).Get();

        if (await _userRoleReadOnlyRepository.GetByUserIdAndRoleIdAsync(user.Id, userRole.Id) == null)
        {
            await _roleDomainService.AddUserAsync(userRole, user);

            await _roleOperationRepository.UpdateAsync(userRole);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed user roles successfully");
    }
}