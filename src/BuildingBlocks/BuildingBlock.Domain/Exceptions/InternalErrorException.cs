using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class InternalErrorException : BaseException
{
    public InternalErrorException(string code, string message) : base(ExceptionStatusCode.InternalError, message)
    {
    }
}