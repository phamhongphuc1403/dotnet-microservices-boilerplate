using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

public record DeleteManyProductsCommand(DeleteManyProductsDto Dto) : ICommand
{
}