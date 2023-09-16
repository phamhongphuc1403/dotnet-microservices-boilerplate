using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class OperationUnauthorizedException : BaseException
{
    public OperationUnauthorizedException(string code, string message) : base(ExceptionStatusCode.OperationUnauthorized,
        message)
    {
    }
}