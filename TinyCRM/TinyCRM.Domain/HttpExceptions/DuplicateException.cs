using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class DuplicateException : HttpException
    {
        public DuplicateException(string message = "Duplicate record") : base(HttpStatusCode.Conflict, ExceptionEnum.Duplicate, message)
        {
        }
    }
}