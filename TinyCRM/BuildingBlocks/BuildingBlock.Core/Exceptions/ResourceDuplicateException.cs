using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class ResourceDuplicateException : BaseException
{
    public ResourceDuplicateException(string message) : base(ExceptionStatusCode.ResourceDuplicate,
        message)
    {
    }
}