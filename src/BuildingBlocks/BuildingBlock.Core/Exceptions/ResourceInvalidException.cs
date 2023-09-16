using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class ResourceInvalidException : BaseException
{
    public ResourceInvalidException(string code, string message) : base(ExceptionStatusCode.ResourceInvalid, message)
    {
    }
}