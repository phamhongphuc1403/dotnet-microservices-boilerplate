using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class ResourceAccessDeniedException : BaseException
{
    public ResourceAccessDeniedException(string code, string message) : base(ExceptionStatusCode.ResourceAccessDenied, message)
    {
    }
}