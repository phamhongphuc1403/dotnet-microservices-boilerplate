using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identities.Domain.RoleAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identities.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : EntityNotFoundException
{
    public RoleNotFoundException(string column, string value) : base(nameof(Role), column, value)
    {
    }

    public RoleNotFoundException(string id) : base(nameof(User), id)
    {
    }
}