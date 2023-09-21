using BuildingBlock.Domain.Exceptions;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.Domain.Exceptions;

public class ProductConflictExceptionException : EntityConflictException
{
    public ProductConflictExceptionException(string column, string value) : base(nameof(Product), column, value)
    {
    }
}