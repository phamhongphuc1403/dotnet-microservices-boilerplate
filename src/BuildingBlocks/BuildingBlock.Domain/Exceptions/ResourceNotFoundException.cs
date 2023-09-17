using BuildingBlock.Domain.Exceptions.Enums;

namespace BuildingBlock.Domain.Exceptions;

public class ResourceNotFoundException : BaseException
{
    public ResourceNotFoundException(string message) : base(ExceptionStatusCode.ResourceNotFound, message)
    {
    }
}