using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;

namespace TinyCRM.Products.Application;

public class ProductSeeder : IDataSeeder
{
    private readonly ILogger<ProductSeeder> _logger;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IReadOnlyRepository<Product> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductSeeder(IOperationRepository<Product> operationRepository,
        IReadOnlyRepository<Product> readonlyRepository, IUnitOfWork unitOfWork, ILogger<ProductSeeder> logger)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readonlyRepository = readonlyRepository;
    }

    public async Task SeedDataAsync()
    {
        if (await _readonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Product data already seeded!");
            return;
        }

        var products = GenerateProducts();

        await _operationRepository.AddRangeAsync(products);

        await _unitOfWork.SaveChangesAsync();

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
            .RuleFor(product => product.CreatedAt, f => f.Date.Past());

        return faker.Generate(50);
    }
}