using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.API.Modules.DealProduct.Services;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

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
        public async Task<ActionResult<PaginationResponse<GetAllDealsDto>>> GetAllAsync([FromQuery] DealQueryDTO query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetLeadDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetDealDto>> UpdateAsync(Guid id, [FromBody] UpdateDealDto dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpPost("{id}/close-as-won")]
        public async Task<ActionResult<GetDealDto>> CloseAsWonAsync(Guid id)
        {
            return Ok(await _service.CloseAsWonAsync(id));
        }

        [HttpPost("{id}/close-as-lost")]
        public async Task<ActionResult<GetDealDto>> CloseAsLostAsync(Guid id)
        {
            return Ok(await _service.CloseAsLostAsync(id));
        }

        [HttpGet("{dealId}/products")]
        public async Task<ActionResult<PaginationResponse<GetDealProductDto>>> GetProductsAsync(Guid dealId, [FromQuery] DealProductDTO query)
        {
            return Ok(await _dealProductService.GetAllAsync(dealId, query));
        }

        [HttpPost("{dealId}/products")]
        public async Task<ActionResult<GetDealDto>> AddProductAsync(Guid dealId, [FromBody] AddOrUpdateProductToDealDto dto)
        {
            return Ok(await _dealProductService.AddAsync(dto, dealId));
        }

        [HttpGet("{dealId}/products/{id}")]
        public async Task<ActionResult<GetDealProductDto>> GetProductByIdAsync(Guid dealId, Guid id)
        {
            return Ok(await _dealProductService.GetByIdAsync(dealId, id));
        }

        [HttpPut("{dealId}/products/{id}")]
        public async Task<ActionResult<GetDealDto>> UpdateProductAsync(Guid dealId, Guid id, [FromBody] AddOrUpdateProductToDealDto dto)
        {
            return Ok(await _dealProductService.UpdateAsync(dto, dealId, id));
        }
    }
}