using TinyCRM.Core.Exceptions;

namespace TinyCRM.ProductManagement.Domain.Exceptions;

public class ProductDuplicateException : ResourceDuplicateException
{
    public ProductDuplicateException(string code) : base($"Product with the code {code} already exists.")
    {
    }
}