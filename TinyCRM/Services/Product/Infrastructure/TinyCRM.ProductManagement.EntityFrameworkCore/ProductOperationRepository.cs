using TinyCRM.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.EntityFrameworkCore;

public class ProductOperationRepository : OperationRepository<Product>, IProductOperationRepository
{
    public ProductOperationRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }
}