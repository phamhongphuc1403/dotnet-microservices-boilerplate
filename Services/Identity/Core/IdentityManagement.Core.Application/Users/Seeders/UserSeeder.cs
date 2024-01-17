using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Users.Seeders;

public class UserSeeder : IDataSeeder
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<UserSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UserSeeder(IUserReadOnlyRepository userReadOnlyRepository, IUserOperationRepository userOperationRepository,
        ILogger<UserSeeder> logger, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userOperationRepository = userOperationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public int ExecutionOrder => 1;

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding users");

        await SeedUserAsync("admin@123", "Admin", "Admin@123");

        await SeedUserAsync("user@123", "User", "User@123");

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed users successfully");
    }

    private async Task SeedUserAsync(string email, string name, string password)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        var admin = await _userReadOnlyRepository.CheckIfExistAsync(userEmailExactMatchSpecification, true);

        if (admin is false) await _userOperationRepository.CreateAsync(new User(email, name), password);
    }
}