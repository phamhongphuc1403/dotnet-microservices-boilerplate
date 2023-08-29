using TinyCRM.Core.CQRS;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Domain.Entities.Enums;

namespace TinyCRM.ProductManagement.Application.Commands.Requests;

public class CreateProductCommand : IQuery<ProductDto>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public ProductTypes Type { get; private set; }

    public CreateProductCommand(CreateOrEditProductDto dto)
    {
        Code = dto.Code;
        Name = dto.Name;
        Price = dto.Price;
        IsAvailable = dto.IsAvailable;
        Type = dto.Type;
    }
}