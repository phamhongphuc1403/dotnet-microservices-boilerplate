using BuildingBlock.Core.Domain.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Exceptions;

public class RoleConflictException : EntityConflictException
{
    public RoleConflictException(string value) : base(nameof(Role), "name", value)
    {
    }
}