using BuildingBlock.Core.Domain.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : EntityNotFoundException
{
    public RoleNotFoundException(string value) : base(nameof(Role), "name", value)
    {
    }
}