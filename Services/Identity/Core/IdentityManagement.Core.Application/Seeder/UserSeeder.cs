using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Seeder;

public class UserSeeder : IDataSeeder
{
    private readonly ILogger<UserSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UserSeeder(IUserReadOnlyRepository userReadOnlyRepository, IUserOperationRepository userOperationRepository,
        ILogger<UserSeeder> logger, IUnitOfWork unitOfWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userOperationRepository = userOperationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public int ExecutionOrder => 1;

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding users");

        var admin = await _userReadOnlyRepository.GetByEmailAsync("admin@123");

        if (admin == null) await _userOperationRepository.CreateAsync(new User("admin@123"), "Admin@123");

        var user = await _userReadOnlyRepository.GetByEmailAsync("user@123");

        if (user == null) await _userOperationRepository.CreateAsync(new User("user@123"), "User@123");

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed users successfully");
    }
}