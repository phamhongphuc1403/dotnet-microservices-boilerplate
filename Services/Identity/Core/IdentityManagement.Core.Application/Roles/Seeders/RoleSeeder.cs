using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace IdentityManagement.Core.Application.Roles.Seeders;

public class RoleSeeder : IDataSeeder
{
    private readonly ILogger<RoleSeeder> _logger;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleSeeder(IRoleReadOnlyRepository roleReadOnlyRepository, IRoleOperationRepository roleOperationRepository,
        ILogger<RoleSeeder> logger, IUnitOfWork unitOfWork)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _roleOperationRepository = roleOperationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public int ExecutionOrder => 1;

    public async Task SeedDataAsync()
    {
        _logger.LogInformation("Start seeding roles");

        var admin = await _roleReadOnlyRepository.GetByNameAsync("admin");

        if (admin == null) await _roleOperationRepository.CreateAsync(new Role("admin"));

        var user = await _roleReadOnlyRepository.GetByNameAsync("user");

        if (user == null) await _roleOperationRepository.CreateAsync(new Role("user"));

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Seed roles successfully");
    }
}