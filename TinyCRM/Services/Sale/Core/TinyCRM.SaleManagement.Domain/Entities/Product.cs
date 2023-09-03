using TinyCRM.Core;

namespace TinyCRM.SaleManagement.Domain.Entities;

public class Product : GuidBaseEntity
{
    public string Code { get; private set; } = null!;
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}