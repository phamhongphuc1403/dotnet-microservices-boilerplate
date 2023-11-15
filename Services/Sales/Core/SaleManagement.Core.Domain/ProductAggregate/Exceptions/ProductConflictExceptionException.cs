using BuildingBlock.Core.Domain.Exceptions;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Core.Domain.ProductAggregate.Exceptions;

public class ProductConflictExceptionException : EntityConflictException
{
    public ProductConflictExceptionException(string column, string value) : base(nameof(Product), column, value)
    {
    }

    public ProductConflictExceptionException(Guid id) : base(id)
    {
    }
}