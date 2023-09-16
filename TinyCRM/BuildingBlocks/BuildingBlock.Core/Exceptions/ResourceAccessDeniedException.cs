using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class ResourceAccessDeniedException : BaseException
{
    public ResourceAccessDeniedException(string code, string message) : base(ExceptionStatusCode.ResourceAccessDenied, message)
    {
    }
}