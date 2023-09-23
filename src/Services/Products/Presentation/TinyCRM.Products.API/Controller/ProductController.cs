using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Products.Application.CQRS.Commands.Requests;
using TinyCRM.Products.Application.CQRS.Queries.Requests;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.API.Controller;

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
    public async Task<ActionResult<FilterAndPagingResultDto<ProductDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingProductsDto dto)
    {
        var products = await _mediator.Send(new FilterAndPagingProductsQuery(dto));

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] CreateOrEditProductDto dto)
    {
        var product = await _mediator.Send(new CreateProductCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
    }
}