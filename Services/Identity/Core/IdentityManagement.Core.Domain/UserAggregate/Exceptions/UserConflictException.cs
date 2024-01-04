using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Constants;

namespace IdentityManagement.Core.Domain.UserAggregate.Exceptions;

public class UserConflictException : EntityConflictException
{
    public UserConflictException(string column, string value) : base(nameof(Permissions.User), column, value)
    {
    }
}