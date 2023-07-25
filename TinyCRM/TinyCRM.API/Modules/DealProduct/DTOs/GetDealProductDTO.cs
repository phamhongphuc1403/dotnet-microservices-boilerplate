using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.DealProduct.DTOs
{
    public class GetDealProductDto : GuidBaseEntity
    {
        public Guid ProductId { get; set; }
        public string StringId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double PricePerUnit { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}