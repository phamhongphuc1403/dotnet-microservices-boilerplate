using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class ForbiddenException : HttpException
    {
        public ForbiddenException(string message = "Forbidden") : base(HttpStatusCode.Forbidden, ExceptionEnum.Forbidden, message)
        {
        }
    }
}
