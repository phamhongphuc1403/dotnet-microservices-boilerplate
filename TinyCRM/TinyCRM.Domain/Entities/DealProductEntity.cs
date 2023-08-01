namespace TinyCRM.Domain.Entities
{
    public class DealProductEntity : GuidBaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; } = null!;
        public Guid DealId { get; set; }
        public virtual DealEntity? Deal { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
    }
}