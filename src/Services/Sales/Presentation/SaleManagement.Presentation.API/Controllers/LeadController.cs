using BuildingBlock.Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaleManagement.Core.Application.CQRS.Queries.LeadQueries.Requests;
using SaleManagement.Core.Application.DTOs.LeadDTOs;

namespace SaleManagement.Presentation.API.Controllers;

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