using System.ComponentModel.DataAnnotations;

namespace TinyCRM.API.Modules.DealProduct.DTOs
{
    public class AddOrUpdateProductToDealDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double PricePerUnit { get; set; }
    }
}
