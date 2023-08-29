using TinyCRM.Core.Repositories;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Domain.Repositories;

public interface IProductOperationRepository : IOperationRepository<Product>
{
}