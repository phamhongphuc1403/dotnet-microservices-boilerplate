using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;
using ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;
using ProductManagement.Core.Application.DTOs.ProductDTOs;

namespace ProductManagement.Presentation.API.Controller;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Product.View)]
    public async Task<ActionResult<FilterAndPagingResultDto<ProductSummaryDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingProductsDto dto)
    {
        var products = await _mediator.Send(new FilterAndPagingProductsQuery(dto));

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [Authorize(Policy = Permissions.Product.View)]
    public async Task<ActionResult<ProductDetailDto>> GetByIdAsync(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Product.Create)]
    public async Task<ActionResult<ProductDetailDto>> CreateAsync([FromBody] CreateOrEditProductDto dto)
    {
        var product = await _mediator.Send(new CreateProductCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = Permissions.Product.Edit)]
    public async Task<ActionResult<ProductDetailDto>> EditAsync([FromBody] CreateOrEditProductDto dto, Guid id)
    {
        var product = await _mediator.Send(new EditProductCommand(id, dto));

        return product;
    }

    [HttpDelete]
    [Authorize(Policy = Permissions.Product.Delete)]
    public async Task<ActionResult> DeleteAsync(DeleteManyProductsDto dto)
    {
        await _mediator.Send(new DeleteManyProductsCommand(dto));

        return NoContent();
    }
}