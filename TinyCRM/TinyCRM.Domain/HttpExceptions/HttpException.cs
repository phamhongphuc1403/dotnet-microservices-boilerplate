using System.Net;

namespace TinyCRM.Domain.HttpExceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Response { get; set; }

        public HttpException(HttpStatusCode statusCode, string code, string message) : base(message)
        {
            StatusCode = statusCode;

            Response = $@"
            {{
                ""statusCode"": {(int)statusCode},
                ""code"": ""{code}"",
                ""message"": ""{message}""
            }}";
        }
    }
}