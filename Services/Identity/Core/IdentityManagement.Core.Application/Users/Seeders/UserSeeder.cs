using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Application.Users.IntegrationEvents.Events;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
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

        var admin = await _userReadOnlyRepository.GetByEmailAsync("admin@123", null, true);

        if (admin == null)
            await _userOperationRepository.CreateAsync(new User("admin@123", "Admin"), "Admin@123");

        var user = await _userReadOnlyRepository.GetByEmailAsync("user@123", null, true);

        if (user == null)
        {
            user = new User("user@123", "User");

            await _userOperationRepository.CreateAsync(user, "User@123");

            await _unitOfWork.SaveChangesAsync();

            var userCreationDto = await _userReadOnlyRepository.GetByEmailAsync<UserCreationDto>("user@123");

            _eventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.AvatarUrl, user.CoverUrl,
                userCreationDto!.CreatedAt, userCreationDto.CreatedBy));
        }
        else
        {
            await _unitOfWork.SaveChangesAsync();
        }

        _logger.LogInformation("Seed users successfully");
    }
}