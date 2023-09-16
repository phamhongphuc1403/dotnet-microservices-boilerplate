using TinyCRM.Domain.Enums;

namespace TinyCRM.Domain.Entities;

public class ProductEntity : GuidBaseEntity
{
    public string StringId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public ProductTypes Type { get; set; }
    public virtual ICollection<DealProductEntity> DealsProducts { get; set; } = new HashSet<DealProductEntity>();
}