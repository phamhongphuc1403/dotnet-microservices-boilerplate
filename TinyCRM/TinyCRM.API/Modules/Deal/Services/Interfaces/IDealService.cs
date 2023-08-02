using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Utilities.PaginationHelper;

namespace TinyCRM.API.Modules.Deal.Services.Interfaces
{
    public interface IDealService
    {
        Task<PaginationResponseDTO<GetAllDealsDTO>> GetAllAsync(DealQueryDTO query);

        Task<GetDealDTO> GetByIdAsync(Guid id);

        Task<GetDealDTO> UpdateAsync(Guid id, UpdateDealDTO dto);

        Task<GetDealDTO> CloseAsWonAsync(Guid id);

        Task<GetDealDTO> CloseAsLostAsync(Guid id);

        Task<PaginationResponseDTO<GetAllDealsDTO>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDTO query);
    }
}