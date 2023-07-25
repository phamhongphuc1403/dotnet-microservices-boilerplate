using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string message = "Not found") : base(HttpStatusCode.NotFound, ExceptionEnum.NotFound, message)
        {
        }
    }
}