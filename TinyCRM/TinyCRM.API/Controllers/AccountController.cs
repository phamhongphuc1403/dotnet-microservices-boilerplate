using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IList<GetAccountDto>>> GetAllAsync([FromQuery] AccountQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetAccountDto>> CreateAsync([FromBody] AddOrUpdateAccountDto model)
        {
            var newAccount = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newAccount.Id }, newAccount);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GetAccountDto>> UpdateAsync(Guid id, [FromBody] AddOrUpdateAccountDto model)
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
        public async Task<ActionResult<PaginationResponse<GetContactDto>>> GetAllContactsByIdAsync(Guid id, [FromQuery] ContactQueryDTO query)
        {
            return Ok(await _contactService.GetAllByAccountIdAsync(id, query));
        }

        [HttpGet("{id:guid}/deals")]
        public async Task<ActionResult<PaginationResponse<GetAllDealsDto>>> GetAllDealsByIdAsync(Guid id, [FromQuery] DealQueryDTO query)
        {
            return Ok(await _dealService.GetAllByCustomerIdAsync(id, query));
        }

        [HttpGet("{id:guid}/leads")]
        public async Task<ActionResult<PaginationResponse<GetLeadDto>>> GetAllLeadsByIdAsync(Guid id, [FromQuery] LeadQueryDTO query)
        {
            return Ok(await _leadService.GetAllByCustomerIdAsync(id, query));
        }
    }
}