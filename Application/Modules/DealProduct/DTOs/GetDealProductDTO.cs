using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.DealProduct.DTOs
{
    public class GetDealProductDTO : GuidBaseEntity
    {
        public Guid ProductId { get; set; }
        public string StringId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double PricePerUnit { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}