using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;
using SaleManagement.Core.Application.DTOs.ProductDTOs;

namespace SaleManagement.Presentation.API.Controllers;

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
}