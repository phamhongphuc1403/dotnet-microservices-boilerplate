using System.Net;

namespace TinyCRM.Domain.HttpExceptions;

public class UnauthorizedException : HttpException
{
    public UnauthorizedException(string message = "Unauthorized") : base(HttpStatusCode.Unauthorized,
        ExceptionEnum.Unauthorized, message)
    {
    }
}