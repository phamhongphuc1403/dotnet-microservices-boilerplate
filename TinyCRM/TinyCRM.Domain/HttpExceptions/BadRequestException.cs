using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message = "Bad request") : base(HttpStatusCode.BadRequest, ExceptionEnum.BadRequest, message)
        {
        }
    }
}