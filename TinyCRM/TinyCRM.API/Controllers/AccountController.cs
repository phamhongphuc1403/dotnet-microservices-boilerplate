using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Account.DTOs;
using TinyCRM.Application.Modules.Account.Services.Interfaces;
using TinyCRM.Domain.Constants;

namespace TinyCRM.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService accountService)
        {
            _service = accountService;
        }

        [HttpGet]
        [Authorize(Policy = Permission.Account.View)]
        public async Task<ActionResult<PaginationResponseDto<GetAccountDto>>> GetAllAsync([FromQuery] AccountQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = Permission.Account.View)]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = Permission.Account.Create)]
        public async Task<ActionResult<GetAccountDto>> CreateAsync([FromBody] AddOrUpdateAccountDto model)
        {
            var newAccount = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newAccount.Id }, newAccount);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Permission.Account.Update)]
        public async Task<ActionResult<GetAccountDto>> UpdateAsync(Guid id, [FromBody] AddOrUpdateAccountDto model)
        {
            var updatedAccount = await _service.UpdateAsync(model, id);

            return Ok(updatedAccount);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Permission.Account.Delete)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}