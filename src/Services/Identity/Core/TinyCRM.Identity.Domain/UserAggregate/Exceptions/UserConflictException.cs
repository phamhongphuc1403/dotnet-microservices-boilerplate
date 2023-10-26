using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Shared.Constants.Identity;

namespace TinyCRM.Identity.Domain.UserAggregate.Exceptions;

public class UserConflictException : EntityConflictException
{
    public UserConflictException(string email) : base(nameof(Permissions.User), "email", email)
    {
    }
}