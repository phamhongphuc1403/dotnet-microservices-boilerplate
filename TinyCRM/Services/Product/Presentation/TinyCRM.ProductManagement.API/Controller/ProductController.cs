using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.Commands.Requests;
using TinyCRM.ProductManagement.Application.DTOs;
using TinyCRM.ProductManagement.Application.Queries.Requests;

namespace TinyCRM.Service.Product.API.Controller;

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
    public async Task<ActionResult<FilterAndPagingResultDto<ProductDto>>> GetAllAsync([FromQuery] FilterAndPagingProductsDto dto)
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

        return CreatedAtAction(nameof(GetByIdAsync), new {id = product.Id}, product);
    }
}

