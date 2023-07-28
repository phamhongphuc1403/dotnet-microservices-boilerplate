using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Constants;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.Account.Services;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Contact.Services;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Authorize(Roles = Role.Admin)]
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
        public async Task<ActionResult<PaginationResponse<GetAccountDTO>>> GetAllAsync([FromQuery] AccountQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GetAccountDTO>> UpdateAsync(Guid id, [FromBody] AddOrUpdateAccountDTO model)
        {
            var updatedAccount = await _service.UpdateAsync(model, id);

            return Ok(updatedAccount);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("{id:guid}/contacts")]
        public async Task<ActionResult<PaginationResponse<GetContactDTO>>> GetAllContactsByIdAsync(Guid id, [FromQuery] ContactQueryDTO query)
        {
            return Ok(await _contactService.GetAllByAccountIdAsync(id, query));
        }

        [HttpGet("{id:guid}/deals")]
        public async Task<ActionResult<PaginationResponse<GetAllDealsDTO>>> GetAllDealsByIdAsync(Guid id, [FromQuery] DealQueryDTO query)
        {
            return Ok(await _dealService.GetAllByCustomerIdAsync(id, query));
        }

        [HttpGet("{id:guid}/leads")]
        public async Task<ActionResult<PaginationResponse<GetLeadDTO>>> GetAllLeadsByIdAsync(Guid id, [FromQuery] LeadQueryDTO query)
        {
            return Ok(await _leadService.GetAllByCustomerIdAsync(id, query));
        }
    }
}