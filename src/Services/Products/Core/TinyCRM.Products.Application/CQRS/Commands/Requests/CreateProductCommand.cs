using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;
using TinyCRM.Products.Domain.Entities.Enums;

namespace TinyCRM.Products.Application.CQRS.Commands.Requests;

public class CreateProductCommand : IQuery<ProductDto>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public ProductType Type { get; private set; }

    public CreateProductCommand(CreateOrEditProductDto dto)
    {
        Code = dto.Code;
        Name = dto.Name;
        Price = dto.Price;
        IsAvailable = dto.IsAvailable;
        Type = dto.Type;
    }
}