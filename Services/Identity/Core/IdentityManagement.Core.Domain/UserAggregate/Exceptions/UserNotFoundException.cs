using BuildingBlock.Core.Domain.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Exceptions;

public class UserNotFoundException : EntityNotFoundException
{
    public UserNotFoundException(string column, string value) : base(nameof(User), column, value)
    {
    }

    public UserNotFoundException(Guid id) : base(nameof(User), id)
    {
    }
}