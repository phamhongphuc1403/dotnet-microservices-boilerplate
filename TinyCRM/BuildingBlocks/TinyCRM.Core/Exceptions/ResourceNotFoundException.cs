using TinyCRM.Core.Exceptions.Enums;

namespace TinyCRM.Core.Exceptions;

public class ResourceNotFoundException : BaseException
{
    public ResourceNotFoundException(string message) : base(ExceptionStatusCode.ResourceNotFound, message)
    {
    }
}