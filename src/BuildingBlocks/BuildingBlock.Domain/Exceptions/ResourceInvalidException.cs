using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class ResourceInvalidException : BaseException
{
    public ResourceInvalidException(string code, string message) : base(ExceptionStatusCode.ResourceInvalid, message)
    {
    }
}