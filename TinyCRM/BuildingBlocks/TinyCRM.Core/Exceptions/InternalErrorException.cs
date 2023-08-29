using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class InternalErrorException : BaseException
{
    public InternalErrorException(string code, string message) : base(ExceptionStatusCode.InternalError, message)
    {
    }
}