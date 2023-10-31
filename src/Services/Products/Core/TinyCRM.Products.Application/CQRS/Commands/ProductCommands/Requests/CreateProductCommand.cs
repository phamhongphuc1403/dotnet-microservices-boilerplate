using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

public class CreateProductCommand : CreateOrEditProductDto, ICommand<ProductDetailDto>
{
    public CreateProductCommand(CreateOrEditProductDto dto) : base(dto)
    {
    }
}