using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.Account.Model;
using TinyCRM.API.Modules.Account.Services;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Contact.Services;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Modules.Lead.Services;

namespace TinyCRM.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IDealService _dealService;
        private readonly IContactService _contactService;
        private readonly ILeadService _leadService;

        public AccountController(IAccountService accountService,
            IDealService dealService, IContactService contactService, ILeadService leadService)
        {
            _service = accountService;
            _dealService = dealService;
            _contactService = contactService;
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GetAccountDTO>>> GetAllAsync([FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? name, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _service.GetAllAsync(skip, take, name, sortBy, descending));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetAccountDTO>> CreateAsync([FromBody] AddOrUpdateAccountDTO model)
        {
            var newAccount = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newAccount.Id }, newAccount);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetAccountDTO>> UpdateAsync(Guid id, [FromBody] AddOrUpdateAccountDTO model)
        {
            var updatedAccount = await _service.UpdateAsync(model, id);

            return Ok(updatedAccount);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("{id}/contacts")]
        public async Task<ActionResult<IList<GetContactDTO>>> GetAllContactsByIdAsync(Guid id, [FromQuery] int? skip,
            [FromQuery] int? take, [FromQuery] string? name, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _contactService.GetAllByAccountIdAsync(id, skip, take, name, sortBy, descending));
        }

        [HttpGet("{id}/deals")]
        public async Task<ActionResult<IList<GetAllDealsDTO>>> GetAllDealsByIdAsync(Guid id,
            [FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? title, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _dealService.GetAllByCustomerIdAsync(id, skip, take, title, sortBy, descending));
        }

        [HttpGet("{id}/leads")]
        public async Task<ActionResult<IList<GetLeadDTO>>> GetAllLeadsByIdAsync(Guid id,
            [FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? title, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _leadService.GetAllByCustomerIdAsync(id, skip, take, title, sortBy, descending));
        }
    }
}