using BuildingBlock.Core.Domain.Exceptions;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Domain.ProductAggregate.Exceptions;

public class ProductConflictExceptionException : EntityConflictException
{
    public ProductConflictExceptionException(string column, string value) : base(nameof(Product), column, value)
    {
    }
}