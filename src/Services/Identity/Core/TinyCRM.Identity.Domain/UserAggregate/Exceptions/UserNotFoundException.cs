using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.Exceptions;

public class UserNotFoundException : EntityNotFoundException
{
    public UserNotFoundException(string column, string value) : base(nameof(User), column, value)
    {
    }

    public UserNotFoundException(Guid id) : base(nameof(User), id)
    {
    }
}