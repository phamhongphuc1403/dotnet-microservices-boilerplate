using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.DealProduct.DTOs;

namespace TinyCRM.Application.Modules.DealProduct.Services.Interfaces;

public interface IDealProductService
{
    Task<GetDealProductDto> AddAsync(AddOrUpdateProductToDealDto dto, Guid id);

    Task<PaginationResponseDto<GetDealProductDto>> GetAllAsync(Guid id, DealProductDto query);

    Task<GetDealProductDto> GetByIdAsync(Guid dealId, Guid id);

    Task<GetDealProductDto> UpdateAsync(AddOrUpdateProductToDealDto dto, Guid dealId, Guid id);
}