using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class ResourceDuplicateException : BaseException
{
    public ResourceDuplicateException(string message) : base(ExceptionStatusCode.ResourceDuplicate,
        message)
    {
    }
}