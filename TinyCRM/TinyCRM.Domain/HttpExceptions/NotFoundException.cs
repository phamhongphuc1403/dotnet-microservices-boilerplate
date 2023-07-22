using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TinyCRM.Domain.HttpExceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string message = "Not found") : base(HttpStatusCode.NotFound, ExceptionEnum.NOT_FOUND, message) { }
    }
}
