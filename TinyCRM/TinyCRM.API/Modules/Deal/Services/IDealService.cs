using TinyCRM.API.Modules.Deal.DTOs;

namespace TinyCRM.API.Modules.Deal.Services
{
    public interface IDealService
    {
        Task<IList<GetAllDealsDTO>> GetAllAsync(int? skip, int? take, string? title, string? sortBy, bool? descending);
        Task<GetDealDTO> GetByIdAsync(Guid id);
        Task<GetDealDTO> UpdateAsync(Guid id, UpdateDealDTO dto);
        Task<GetDealDTO> CloseAsWonAsync(Guid id);
        Task<GetDealDTO> CloseAsLostAsync(Guid id);
        Task<IList<GetAllDealsDTO>> GetAllByCustomerIdAsync(Guid customerId, int? skip, int? take, string? title, string? sortBy, bool? descending);
    }
}