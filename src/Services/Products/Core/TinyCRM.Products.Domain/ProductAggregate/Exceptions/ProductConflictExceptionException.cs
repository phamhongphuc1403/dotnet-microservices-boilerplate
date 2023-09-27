using BuildingBlock.Domain.Exceptions;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Domain.ProductAggregate.Exceptions;

public class ProductConflictExceptionException : EntityConflictException
{
    public ProductConflictExceptionException(string column, string value) : base(nameof(Product), column, value)
    {
    }
}