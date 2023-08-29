using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class ResourceInvalidException : BaseException
{
    public ResourceInvalidException(string code, string message) : base(ExceptionStatusCode.ResourceInvalid, message)
    {
    }
}