using BuildingBlock.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Application.Queries.Requests;

namespace TinyCRM.Sales.API.Controllers;

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