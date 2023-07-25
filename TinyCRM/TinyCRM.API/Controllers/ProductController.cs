using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.API.Modules.Product.Services;
using TinyCRM.Infrastructure.PaginationHelper;

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

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<GetProductDto>>> GetAllAsync([FromQuery] ProductQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetProductDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetProductDto>> CreateAsync([FromBody] AddOrUpdateProductDto model)
        {
            var newProduct = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newProduct.StringId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductDto>> UpdateAsync(Guid id, [FromBody] AddOrUpdateProductDto model)
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