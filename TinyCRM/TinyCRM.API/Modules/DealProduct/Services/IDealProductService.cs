using TinyCRM.API.Modules.DealProduct.DTOs;

namespace TinyCRM.API.Modules.DealProduct.Services
{
    public interface IDealProductService
    {
        Task<GetDealProductDTO> AddAsync(AddOrUpdateProductToDealDTO dto, Guid id);
        Task<IList<GetDealProductDTO>> GetAllAsync(Guid Id, int? skip, int? take, string? name, string? sortBy, bool? descending);
        Task<GetDealProductDTO> GetByIdAsync(Guid dealId, Guid id);
        Task<GetDealProductDTO> UpdateAsync(AddOrUpdateProductToDealDTO dto, Guid DealId, Guid id);
    }
}