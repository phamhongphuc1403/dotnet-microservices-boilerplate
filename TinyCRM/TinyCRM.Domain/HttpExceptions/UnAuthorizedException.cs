using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class UnAuthorizedException : HttpException
    {
        public UnAuthorizedException(string message = "Invalid access token") : base(HttpStatusCode.Unauthorized, ExceptionEnum.UNAUTHORIZED, message) { }
    }
}
