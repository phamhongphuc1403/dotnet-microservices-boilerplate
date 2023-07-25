using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.DealProduct.Services
{
    public interface IDealProductService
    {
        Task<GetDealProductDto> AddAsync(AddOrUpdateProductToDealDto dto, Guid id);

        Task<PaginationResponse<GetDealProductDto>> GetAllAsync(Guid id, DealProductDTO query);

        Task<GetDealProductDto> GetByIdAsync(Guid dealId, Guid id);

        Task<GetDealProductDto> UpdateAsync(AddOrUpdateProductToDealDto dto, Guid dealId, Guid id);
    }
}