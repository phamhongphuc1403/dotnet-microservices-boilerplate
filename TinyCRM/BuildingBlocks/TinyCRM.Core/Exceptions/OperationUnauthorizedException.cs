using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class OperationUnauthorizedException : BaseException
{
    public OperationUnauthorizedException(string code, string message) : base(ExceptionStatusCode.OperationUnauthorized,
        message)
    {
    }
}