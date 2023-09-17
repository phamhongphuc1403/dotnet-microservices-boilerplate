using BuildingBlock.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.EntityFrameworkCore.Repositories;

public class ProductOperationRepository : OperationRepository<ProductDbContext, Product>, IProductOperationRepository
{
    public ProductOperationRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}