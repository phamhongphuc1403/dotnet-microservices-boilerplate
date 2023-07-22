using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.API.Modules.DealProduct.Services;
using TinyCRM.API.Modules.Lead.DTOs;

namespace TinyCRM.API.Controllers
{
    [Route("api/deals")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _service;
        private readonly IDealProductService _dealProductService;
        public DealController(IDealService service, IDealProductService dealProductService)
        {
            _service = service;
            _dealProductService = dealProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GetAllDealsDTO>>> GetAllAsync([FromQuery] int? skip,
            [FromQuery] int? take, [FromQuery] string? title, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _service.GetAllAsync(skip, take, title, sortBy, descending));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetLeadDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetDealDTO>> UpdateAsync(Guid id, [FromBody] UpdateDealDTO dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpPost("{id}/close-as-won")]
        public async Task<ActionResult<GetDealDTO>> CloseAsWonAsync(Guid id)
        {
            return Ok(await _service.CloseAsWonAsync(id));
        }

        [HttpPost("{id}/close-as-lost")]
        public async Task<ActionResult<GetDealDTO>> CloseAsLostAsync(Guid id)
        {
            return Ok(await _service.CloseAsLostAsync(id));
        }

        [HttpGet("{dealId}/products")]
        public async Task<ActionResult<IList<GetDealProductDTO>>> GetProductsAsync(Guid dealId, [FromQuery] int? skip,
            [FromQuery] int? take, [FromQuery] string? name, [FromQuery] string? sortBy, [FromQuery] bool? descending)
        {
            return Ok(await _dealProductService.GetAllAsync(dealId, skip, take, name, sortBy, descending));
        }

        [HttpPost("{dealId}/products")]
        public async Task<ActionResult<GetDealDTO>> AddProductAsync(Guid dealId, [FromBody] AddOrUpdateProductToDealDTO dto)
        {
            return Ok(await _dealProductService.AddAsync(dto, dealId));
        }

        [HttpGet("{dealId}/products/{id}")]
        public async Task<ActionResult<GetDealProductDTO>> GetProductByIdAsync(Guid dealId, Guid id)
        {
            return Ok(await _dealProductService.GetByIdAsync(dealId, id));
        }

        [HttpPut("{dealId}/products/{id}")]
        public async Task<ActionResult<GetDealDTO>> UpdateProductAsync(Guid dealId, Guid id, [FromBody] AddOrUpdateProductToDealDTO dto)
        {
            return Ok(await _dealProductService.UpdateAsync(dto, dealId, id));
        }
    }
}
