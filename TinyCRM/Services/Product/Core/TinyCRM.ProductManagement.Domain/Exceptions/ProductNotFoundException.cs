using TinyCRM.Core.Exceptions;

namespace TinyCRM.ProductManagement.Domain.Exceptions;

public class ProductNotFoundException : ResourceNotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product is not found with the id: " + id)
    {
    }
}