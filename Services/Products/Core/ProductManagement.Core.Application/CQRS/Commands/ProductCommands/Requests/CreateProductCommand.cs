using BuildingBlock.Core.Application.CQRS;
using ProductManagement.Core.Application.DTOs.ProductDTOs;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;

public record CreateProductCommand(CreateOrEditProductDto Dto) : ICommand<ProductDetailDto>;