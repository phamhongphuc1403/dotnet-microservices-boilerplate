using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identities.EntityFrameworkCore;

public class IdentityException : Exception
{
    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<IdentityError> Errors { get; }
}