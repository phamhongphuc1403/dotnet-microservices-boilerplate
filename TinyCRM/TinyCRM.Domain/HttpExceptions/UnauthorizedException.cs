using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException(string message = "Unauthorized") : base(HttpStatusCode.BadRequest, ExceptionEnum.BadRequest, message)
        {
        }
    }
}
