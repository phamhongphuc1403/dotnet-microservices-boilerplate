using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class BaseException : Exception
{
    public ExceptionStatusCode StatusCode { get; private set; }
    public object Response { get; private set; }
    protected BaseException(ExceptionStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;

        Response = new
        {
            statusCode = (int)statusCode,
            message
        };
    }


}