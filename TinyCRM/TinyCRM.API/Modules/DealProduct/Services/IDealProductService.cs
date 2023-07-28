using TinyCRM.API.Modules.DealProduct.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.DealProduct.Services
{
    public interface IDealProductService
    {
        Task<GetDealProductDTO> AddAsync(AddOrUpdateProductToDealDTO dto, Guid id);

        Task<PaginationResponse<GetDealProductDTO>> GetAllAsync(Guid id, DealProductDTO query);

        Task<GetDealProductDTO> GetByIdAsync(Guid dealId, Guid id);

        Task<GetDealProductDTO> UpdateAsync(AddOrUpdateProductToDealDTO dto, Guid dealId, Guid id);
    }
}