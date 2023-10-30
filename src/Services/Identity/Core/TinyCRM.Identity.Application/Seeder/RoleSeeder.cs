using BuildingBlock.Application;
using BuildingBlock.Domain.Shared.Services;
using Microsoft.Extensions.Logging;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;

namespace TinyCRM.Identity.Application.Seeder;

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