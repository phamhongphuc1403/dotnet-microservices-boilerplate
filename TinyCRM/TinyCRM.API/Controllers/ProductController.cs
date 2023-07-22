using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.API.Modules.Product.Services;

namespace TinyCRM.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService accountService)
        {
            _service = accountService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IList<GetProductDTO>>> GetAllAsync([FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? name, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _service.GetAllAsync(skip, take, name, sortBy, descending));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetProductDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost("")]
        public async Task<ActionResult<GetProductDTO>> CreateAsync([FromBody] AddOrUpdateProductDTO model)
        {
            var newProduct = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newProduct.StringId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductDTO>> UpdateAsync(Guid id, [FromBody] AddOrUpdateProductDTO model)
        {
            var updatedProduct = await _service.UpdateAsync(model, id);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
