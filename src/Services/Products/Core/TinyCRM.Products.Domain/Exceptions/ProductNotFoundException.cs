using BuildingBlock.Domain.Exceptions;

namespace TinyCRM.Products.Domain.Exceptions;

public class ProductNotFoundException : ResourceNotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product is not found with the id: " + id)
    {
    }
}