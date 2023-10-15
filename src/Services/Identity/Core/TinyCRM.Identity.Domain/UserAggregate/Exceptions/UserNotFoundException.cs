using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identities.Domain.UserAggregate.Exceptions;

public class UserNotFoundException : EntityNotFoundException
{
    public UserNotFoundException(string column, string value) : base(nameof(User), column, value)
    {
    }

    public UserNotFoundException(Guid id) : base(nameof(User), id)
    {
    }
}