using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class OperationUnauthorizedException : BaseException
{
    public OperationUnauthorizedException(string code, string message) : base(ExceptionStatusCode.OperationUnauthorized,
        message)
    {
    }
}