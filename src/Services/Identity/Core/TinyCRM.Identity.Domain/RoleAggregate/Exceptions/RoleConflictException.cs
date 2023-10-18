using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.Exceptions;

public class RoleConflictException : EntityConflictException
{
    public RoleConflictException(string value) : base(nameof(Role), "name", value)
    {
    }
}