using BuildingBlock.Core.Domain.Exceptions;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : EntityNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id)
    {
    }
}