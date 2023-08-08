using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Deal.Services.Interfaces;
using TinyCRM.Application.Modules.DealProduct.DTOs;
using TinyCRM.Application.Modules.DealProduct.Services.Interfaces;
using TinyCRM.Application.Modules.Lead.DTOs;
using TinyCRM.Domain.Constants;

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
        [Authorize(Policy = Permission.Deal.View)]
        public async Task<ActionResult<PaginationResponseDto<GetAllDealsDto>>> GetAllAsync([FromQuery] DealQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        [Authorize(Policy = Permission.Deal.View)]
        public async Task<ActionResult<GetLeadDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = Permission.Deal.Update)]
        public async Task<ActionResult<GetDealDto>> UpdateAsync(Guid id, [FromBody] UpdateDealDto dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpPost("{id:guid}/close-as-won")]
        [Authorize(Policy = Permission.Deal.Update)]
        public async Task<ActionResult<GetDealDto>> CloseAsWonAsync(Guid id)
        {
            return Ok(await _service.CloseAsWonAsync(id));
        }

        [HttpPost("{id:guid}/close-as-lost")]
        [Authorize(Policy = Permission.Deal.Update)]
        public async Task<ActionResult<GetDealDto>> CloseAsLostAsync(Guid id)
        {
            return Ok(await _service.CloseAsLostAsync(id));
        }

        [HttpGet("{dealId:guid}/products")]
        [Authorize(Policy = Permission.Deal.View)]
        public async Task<ActionResult<PaginationResponseDto<GetDealProductDto>>> GetProductsAsync(Guid dealId, [FromQuery] DealProductDto query)
        {
            return Ok(await _dealProductService.GetAllAsync(dealId, query));
        }

        [HttpPost("{dealId:guid}/products")]
        [Authorize(Policy = Permission.Deal.Update)]
        public async Task<ActionResult<GetDealDto>> AddProductAsync(Guid dealId, [FromBody] AddOrUpdateProductToDealDto dto)
        {
            return Ok(await _dealProductService.AddAsync(dto, dealId));
        }

        [HttpGet("{dealId:guid}/products/{id:guid}")]
        [Authorize(Policy = Permission.Deal.View)]
        public async Task<ActionResult<GetDealProductDto>> GetProductByIdAsync(Guid dealId, Guid id)
        {
            return Ok(await _dealProductService.GetByIdAsync(dealId, id));
        }

        [HttpPut("{dealId:guid}/products/{id:guid}")]
        [Authorize(Policy = Permission.Deal.Update)]
        public async Task<ActionResult<GetDealDto>> UpdateProductAsync(Guid dealId, Guid id, [FromBody] AddOrUpdateProductToDealDto dto)
        {
            return Ok(await _dealProductService.UpdateAsync(dto, dealId, id));
        }

        [HttpGet("customer/{customerId:guid}/deals")]
        [Authorize(Policy = Permission.Deal.View)]
        public async Task<ActionResult<PaginationResponseDto<GetAllDealsDto>>> GetAllByCustomerIdAsync(Guid customerId, [FromQuery] DealQueryDto query)
        {
            return Ok(await _service.GetAllByCustomerIdAsync(customerId, query));
        }
    }
}