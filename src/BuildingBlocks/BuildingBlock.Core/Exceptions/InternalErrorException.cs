using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class InternalErrorException : BaseException
{
    public InternalErrorException(string code, string message) : base(ExceptionStatusCode.InternalError, message)
    {
    }
}