using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identities.Domain.Entities;

namespace TinyCRM.Identities.Domain.Exceptions;

public class UserNotFoundException : EntityNotFoundException
{
    public UserNotFoundException(string column, string value) : base(nameof(User), column, value)
    {
    }
}