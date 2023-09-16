using BuildingBlock.Core.Exceptions.Enums;

namespace BuildingBlock.Core.Exceptions;

public class ResourceNotFoundException : BaseException
{
    public ResourceNotFoundException(string message) : base(ExceptionStatusCode.ResourceNotFound, message)
    {
    }
}