using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Lead.DTOs;

namespace TinyCRM.Application.Modules.Lead.Services.Interfaces
{
    public interface ILeadService
    {
        Task<GetLeadDto> GetByIdAsync(Guid id);

        Task<GetLeadDto> AddAsync(AddLeadDto dto);

        Task<GetLeadDto> UpdateAsync(UpdateLeadDto dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponseDto<GetLeadDto>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDto query);

        Task<PaginationResponseDto<GetLeadDto>> GetAllAsync(LeadQueryDto query);

        Task<GetLeadDto> DisqualifyLeadAsync(Guid id, DisqualifyLeadDto dto);

        Task<GetDealDto> QualifyLeadAsync(Guid id);
    }
}