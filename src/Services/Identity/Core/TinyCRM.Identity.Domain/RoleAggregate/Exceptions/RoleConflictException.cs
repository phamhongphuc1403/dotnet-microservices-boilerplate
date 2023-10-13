using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identities.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identities.Domain.RoleAggregate.Exceptions;

public class RoleConflictException : EntityConflictException
{
    public RoleConflictException(string value) : base(nameof(Role), "name", value)
    {
    }
}