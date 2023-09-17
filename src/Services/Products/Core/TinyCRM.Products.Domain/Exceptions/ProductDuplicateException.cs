using BuildingBlock.Domain.Exceptions;

namespace TinyCRM.Products.Domain.Exceptions;

public class ProductDuplicateException : ResourceDuplicateException
{
    public ProductDuplicateException(string code) : base($"Product with the code {code} already exists.")
    {
    }
}