using System.Net;

namespace TinyCRM.Domain.HttpExceptions;

public class InternalException : HttpException
{
    public InternalException(string message = "Internal server error") : base(HttpStatusCode.InternalServerError,
        ExceptionEnum.Internal, message)
    {
    }
}