using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.Infrastructure.PaginationHelper;

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
        public async Task<ActionResult<PaginationResponse<GetLeadDto>>> GetAllAsync([FromQuery] LeadQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetLeadDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetLeadDto>> CreateAsync([FromBody] AddLeadDto model)
        {
            var newLead = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newLead.Id }, newLead);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetLeadDto>> UpdateAsync(Guid id, [FromBody] UpdateLeadDto model)
        {
            return Ok(await _service.UpdateAsync(model, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("{id}/qualify")]
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

        [HttpPost("{id}/disqualify")]
        public async Task<ActionResult<GetLeadDto>> DisqualifyLeadAsync(Guid id, [FromBody] DisqualifyLeadDto model)
        {
            return Ok(await _service.DisqualifyLeadAsync(id, model));
        }
    }
}