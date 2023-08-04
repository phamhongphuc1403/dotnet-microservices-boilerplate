using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.DealProduct.DTOs;

namespace TinyCRM.Application.Modules.DealProduct.Services.Interfaces
{
    public interface IDealProductService
    {
        Task<GetDealProductDTO> AddAsync(AddOrUpdateProductToDealDTO dto, Guid id);

        Task<PaginationResponseDTO<GetDealProductDTO>> GetAllAsync(Guid id, DealProductDTO query);

        Task<GetDealProductDTO> GetByIdAsync(Guid dealId, Guid id);

        Task<GetDealProductDTO> UpdateAsync(AddOrUpdateProductToDealDTO dto, Guid dealId, Guid id);
    }
}