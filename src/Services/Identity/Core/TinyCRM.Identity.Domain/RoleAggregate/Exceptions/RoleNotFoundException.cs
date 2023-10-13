using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identities.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identities.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : EntityNotFoundException
{
    public RoleNotFoundException(string value) : base(nameof(Role), "name", value)
    {
    }
}