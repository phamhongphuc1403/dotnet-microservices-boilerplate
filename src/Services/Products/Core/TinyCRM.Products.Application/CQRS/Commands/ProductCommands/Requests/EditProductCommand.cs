using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

public class EditProductCommand : CreateOrEditProductDto, ICommand<ProductDetailDto>
{
    public EditProductCommand(Guid id, CreateOrEditProductDto dto) : base(dto)
    {
        Id = id;
    }

    public Guid Id { get; }
}