namespace TinyCRM.Application.Modules.DealProduct.DTOs;

public class AddOrUpdateProductToDealDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public double PricePerUnit { get; set; }
}