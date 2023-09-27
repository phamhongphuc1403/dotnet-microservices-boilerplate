using BuildingBlock.Domain.Exceptions;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : EntityNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id)
    {
    }
}