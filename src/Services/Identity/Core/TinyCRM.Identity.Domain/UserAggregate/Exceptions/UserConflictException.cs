using BuildingBlock.Domain.Constants.Identity;
using BuildingBlock.Domain.Exceptions;

namespace TinyCRM.Identities.Domain.UserAggregate.Exceptions;

public class UserConflictException : EntityConflictException
{
    public UserConflictException(string email) : base(nameof(Permissions.User), "email", email)
    {
    }
}