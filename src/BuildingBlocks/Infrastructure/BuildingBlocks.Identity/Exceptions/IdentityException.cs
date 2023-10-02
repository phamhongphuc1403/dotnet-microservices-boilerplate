using Microsoft.AspNetCore.Identity;

namespace BuildingBlocks.Identity.Exceptions;

public class IdentityException : Exception
{
    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<IdentityError> Errors { get; }
}