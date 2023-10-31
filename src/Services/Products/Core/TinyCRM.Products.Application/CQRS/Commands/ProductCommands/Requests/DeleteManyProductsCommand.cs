using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

public class DeleteManyProductsCommand : DeleteManyProductsDto, ICommand
{
    public DeleteManyProductsCommand(DeleteManyProductsDto dto) : base(dto)
    {
    }
}