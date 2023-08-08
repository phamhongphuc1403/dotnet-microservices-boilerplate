using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Lead.DTOs;
using TinyCRM.Application.Modules.Lead.Services.Interfaces;
using TinyCRM.Domain.Constants;

namespace TinyCRM.API.Controllers
{
    [Route("api/leads")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _service;

        public LeadController(ILeadService leadService)
        {
            _service = leadService;
        }

        [HttpGet]
        [Authorize(Policy = Permission.Lead.View)]
        public async Task<ActionResult<PaginationResponseDto<GetLeadDto>>> GetAllAsync([FromQuery] LeadQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        [Authorize(Policy = Permission.Lead.View)]
        public async Task<ActionResult<GetLeadDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = Permission.Lead.Create)]
        public async Task<ActionResult<GetLeadDto>> CreateAsync([FromBody] AddLeadDto model)
        {
            var newLead = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newLead.Id }, newLead);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Permission.Lead.Update)]
        public async Task<ActionResult<GetLeadDto>> UpdateAsync(Guid id, [FromBody] UpdateLeadDto model)
        {
            return Ok(await _service.UpdateAsync(model, id));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Permission.Lead.Delete)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("{id:guid}/qualify")]
        [Authorize(Policy = Permission.Lead.Update)]
        public async Task<ActionResult<GetDealDto>> QualifyLeadAsync(Guid id)
        {
            var deal = await _service.QualifyLeadAsync(id);

            var routeValues = new
            {
                action = nameof(DealController.GetByIdAsync),
                controller = "Deal",
                id = deal.Id,
            };

            return CreatedAtRoute(routeValues, deal);
        }

        [HttpPost("{id:guid}/disqualify")]
        [Authorize(Policy = Permission.Lead.Update)]
        public async Task<ActionResult<GetLeadDto>> DisqualifyLeadAsync(Guid id, [FromBody] DisqualifyLeadDto model)
        {
            return Ok(await _service.DisqualifyLeadAsync(id, model));
        }

        [HttpGet("customer/{customerId:guid}/leads")]
        [Authorize(Policy = Permission.Lead.View)]
        public async Task<ActionResult<PaginationResponseDto<GetLeadDto>>> GetAllLeadsByIdAsync(Guid customerId, [FromQuery] LeadQueryDto query)
        {
            return Ok(await _service.GetAllByCustomerIdAsync(customerId, query));
        }
    }
}