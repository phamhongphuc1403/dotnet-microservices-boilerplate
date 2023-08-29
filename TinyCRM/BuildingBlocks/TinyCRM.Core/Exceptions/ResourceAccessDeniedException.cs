using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class ResourceAccessDeniedException : BaseException
{
    public ResourceAccessDeniedException(string code, string message) : base(ExceptionStatusCode.ResourceAccessDenied, message)
    {
    }
}