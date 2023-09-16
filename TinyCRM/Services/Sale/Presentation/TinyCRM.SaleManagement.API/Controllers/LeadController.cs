using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuildingBlock.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Application.Queries.Requests;

namespace TinyCRM.SaleManagement.API.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<FilterAndPagingResultDto<LeadDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingLeadsDto dto)
    {
        var leads = await _mediator.Send(new FilterAndPagingLeadsQuery(dto));

        return Ok(leads);
    }
}