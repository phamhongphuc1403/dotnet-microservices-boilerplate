using System.Net;
using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

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