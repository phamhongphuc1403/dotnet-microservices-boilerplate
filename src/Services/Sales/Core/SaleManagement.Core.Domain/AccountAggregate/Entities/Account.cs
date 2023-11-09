using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.LeadAggregate.Entities;

namespace SaleManagement.Core.Domain.AccountAggregate.Entities;

public class Account : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public string Email { get; private set; } = null!;
    public ICollection<Lead> Leads { get; private set; } = null!;
}