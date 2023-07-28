using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Lead.Services
{
    public interface ILeadService
    {
        Task<GetLeadDTO> GetByIdAsync(Guid id);

        Task<GetLeadDTO> AddAsync(AddLeadDTO dto);

        Task<GetLeadDTO> UpdateAsync(UpdateLeadDTO dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponse<GetLeadDTO>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDTO query);

        Task<PaginationResponse<GetLeadDTO>> GetAllAsync(LeadQueryDTO query);

        Task<GetLeadDTO> DisqualifyLeadAsync(Guid id, DisqualifyLeadDTO model);

        Task<GetDealDTO> QualifyLeadAsync(Guid id);
    }
}