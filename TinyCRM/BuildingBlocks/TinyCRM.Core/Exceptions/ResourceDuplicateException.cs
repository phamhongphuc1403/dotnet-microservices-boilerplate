using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class ResourceDuplicateException : BaseException
{
    public ResourceDuplicateException(string message) : base(ExceptionStatusCode.ResourceDuplicate,
        message)
    {
    }
}