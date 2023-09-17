using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Repositories;

namespace TinyCRM.Products.EntityFrameworkCore.Repositories;

public class ProductOperationRepository : OperationRepository<ProductDbContext, Product>, IProductOperationRepository
{
    public ProductOperationRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}