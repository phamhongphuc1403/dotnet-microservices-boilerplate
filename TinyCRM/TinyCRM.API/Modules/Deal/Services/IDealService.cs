using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Deal.Services
{
    public interface IDealService
    {
        Task<PaginationResponse<GetAllDealsDTO>> GetAllAsync(DealQueryDTO query);

        Task<GetDealDTO> GetByIdAsync(Guid id);

        Task<GetDealDTO> UpdateAsync(Guid id, UpdateDealDTO dto);

        Task<GetDealDTO> CloseAsWonAsync(Guid id);

        Task<GetDealDTO> CloseAsLostAsync(Guid id);

        Task<PaginationResponse<GetAllDealsDTO>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDTO query);
    }
}