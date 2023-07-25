using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Lead.Services
{
    public interface ILeadService
    {
        Task<GetLeadDto> GetByIdAsync(Guid id);

        Task<GetLeadDto> AddAsync(AddLeadDto dto);

        Task<GetLeadDto> UpdateAsync(UpdateLeadDto dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponse<GetLeadDto>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDTO query);

        Task<PaginationResponse<GetLeadDto>> GetAllAsync(LeadQueryDTO query);

        Task<GetLeadDto> DisqualifyLeadAsync(Guid id, DisqualifyLeadDto model);

        Task<GetDealDto> QualifyLeadAsync(Guid id);
    }
}