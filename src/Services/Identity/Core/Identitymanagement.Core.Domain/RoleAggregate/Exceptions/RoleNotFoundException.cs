using BuildingBlock.Core.Domain.Exceptions;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : EntityNotFoundException
{
    public RoleNotFoundException(string value) : base(nameof(Role), "name", value)
    {
    }
}