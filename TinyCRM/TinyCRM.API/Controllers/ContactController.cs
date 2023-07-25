using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Contact.Services;

namespace TinyCRM.API.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService contactService)
        {
            _service = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GetContactDto>>> GetAllAsync([FromQuery] ContactQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetContactDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetContactDto>> CreateAsync([FromBody] AddOrUpdateContactDto model)
        {
            var newContact = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetContactDto>> UpdateAsync(Guid id, [FromBody] AddOrUpdateContactDto model)
        {
            var updatedContact = await _service.UpdateAsync(model, id);

            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}