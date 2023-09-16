using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuildingBlock.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Application.Queries.Requests;

namespace TinyCRM.SaleManagement.API.Controllers;

[ApiController]
[Route("api/deals")]
public class DealController : ControllerBase
{
    private readonly IMediator _mediator;

    public DealController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<FilterAndPagingResultDto<DealDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingDealsDto dto)

    {
        var deals = await _mediator.Send(new FilterAndPagingDealsQuery(dto));

        return Ok(deals);
    }
}