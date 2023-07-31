using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Constants;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Controllers
{
    [Route("api/leads")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _service;

        public LeadController(ILeadService leadService)
        {
            _service = leadService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<GetLeadDTO>>> GetAllAsync([FromQuery] LeadQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetLeadDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetLeadDTO>> CreateAsync([FromBody] AddLeadDTO model)
        {
            var newLead = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newLead.Id }, newLead);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GetLeadDTO>> UpdateAsync(Guid id, [FromBody] UpdateLeadDTO model)
        {
            return Ok(await _service.UpdateAsync(model, id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("{id:guid}/qualify")]
        public async Task<ActionResult<GetDealDTO>> QualifyLeadAsync(Guid id)
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
        public async Task<ActionResult<GetLeadDTO>> DisqualifyLeadAsync(Guid id, [FromBody] DisqualifyLeadDTO model)
        {
            return Ok(await _service.DisqualifyLeadAsync(id, model));
        }

        [HttpGet("customer/{customerId:guid}/leads")]
        public async Task<ActionResult<PaginationResponse<GetLeadDTO>>> GetAllLeadsByIdAsync(Guid customerId, [FromQuery] LeadQueryDTO query)
        {
            return Ok(await _service.GetAllByCustomerIdAsync(customerId, query));
        }
    }
}