using Bogus;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using Microsoft.Extensions.Logging;
using ProductManagement.Core.Application.IntegrationEvents.Events;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Application;

public class ProductSeeder : IDataSeeder
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductSeeder> _logger;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IReadOnlyRepository<Product> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductSeeder(IOperationRepository<Product> operationRepository,
        IReadOnlyRepository<Product> readonlyRepository, IUnitOfWork unitOfWork, ILogger<ProductSeeder> logger,
        IEventBus eventBus)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _eventBus = eventBus;
        _readonlyRepository = readonlyRepository;
    }

    public async Task SeedDataAsync()
    {
        if (await _readonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Product data already seeded!");
            return;
        }

        var products = GenerateProducts().ToList();

        await _operationRepository.AddRangeAsync(products);

        await _unitOfWork.SaveChangesAsync();

        foreach (var product in products)
            _eventBus.Publish(new ProductCreatedIntegrationEvent(product.Id, product.Code, product.Name,
                product.Price, product.IsAvailable, product.Type, product.CreatedAt, "guest"));

        _logger.LogInformation("Product data seeded successfully!");
    }

    private static IEnumerable<Product> GenerateProducts()
    {
        var faker = new Faker<Product>()
            .RuleFor(product => product.Code, f => f.Commerce.Ean13())
            .RuleFor(product => product.Name, f => f.Commerce.ProductName())
            .RuleFor(product => product.Id, f => f.Random.Guid())
            .RuleFor(product => product.Price, f => f.Random.Double(100, 10000))
            .RuleFor(product => product.IsAvailable, f => f.Random.Bool())
            .RuleFor(product => product.Type, f => f.PickRandom<ProductType>())
            .RuleFor(product => product.CreatedAt, f => DateTime.UtcNow);

        return faker.Generate(50);
    }
}