using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;

namespace TinyCRM.Application.Modules.Deal.Services.Interfaces
{
    public interface IDealService
    {
        Task<PaginationResponseDto<GetAllDealsDto>> GetAllAsync(DealQueryDto query);

        Task<GetDealDto> GetByIdAsync(Guid id);

        Task<GetDealDto> UpdateAsync(Guid id, UpdateDealDto dto);

        Task<GetDealDto> CloseAsWonAsync(Guid id);

        Task<GetDealDto> CloseAsLostAsync(Guid id);

        Task<PaginationResponseDto<GetAllDealsDto>> GetAllByCustomerIdAsync(Guid customerId, DealQueryDto query);
    }
}