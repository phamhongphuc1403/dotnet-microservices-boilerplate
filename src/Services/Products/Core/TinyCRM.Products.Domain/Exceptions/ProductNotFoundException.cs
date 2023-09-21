using BuildingBlock.Domain.Exceptions;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.Domain.Exceptions;

public class ProductNotFoundException : EntityNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id)
    {
    }
}