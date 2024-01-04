namespace BuildingBlock.Core.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("You do not have permission to access this resource.")
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }
}