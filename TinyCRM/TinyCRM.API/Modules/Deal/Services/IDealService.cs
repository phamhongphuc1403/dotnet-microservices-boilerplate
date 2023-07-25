using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Deal.Services
{
    public interface IDealService
    {
        Task<PaginationResponse<GetAllDealsDto>> GetAllAsync(DealQueryDTO query);

        Task<GetDealDto> GetByIdAsync(Guid id);

        Task<GetDealDto> UpdateAsync(Guid id, UpdateDealDto dto);

        Task<GetDealDto> CloseAsWonAsync(Guid id);

        Task<GetDealDto> CloseAsLostAsync(Guid id);

        Task<PaginationResponse<GetAllDealsDto>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDTO query);
    }
}