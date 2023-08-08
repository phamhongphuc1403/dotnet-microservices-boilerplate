using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Product.DTOs;
using TinyCRM.Application.Modules.Product.Services.Interfaces;
using TinyCRM.Domain.Constants;

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
        [Authorize(Policy = Permission.Product.View)]
        public async Task<ActionResult<PaginationResponseDto<GetProductDto>>> GetAllAsync([FromQuery] ProductQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        [Authorize(Policy = Permission.Product.View)]
        public async Task<ActionResult<GetProductDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = Permission.Product.Create)]
        public async Task<ActionResult<GetProductDto>> CreateAsync([FromBody] AddOrUpdateProductDto model)
        {
            var newProduct = await _service.AddAsync(model);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newProduct.StringId }, newProduct);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Permission.Product.Update)]
        public async Task<ActionResult<GetProductDto>> UpdateAsync(Guid id, [FromBody] AddOrUpdateProductDto model)
        {
            var updatedProduct = await _service.UpdateAsync(model, id);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Permission.Product.Delete)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}