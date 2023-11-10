using BuildingBlock.Core.Application.CQRS;
using ProductManagement.Core.Application.DTOs;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;

public record DeleteManyProductsCommand(DeleteManyProductsDto Dto) : ICommand
{
}