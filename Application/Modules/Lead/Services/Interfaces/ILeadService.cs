using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Deal.DTOs;
using TinyCRM.Application.Modules.Lead.DTOs;

namespace TinyCRM.Application.Modules.Lead.Services.Interfaces
{
    public interface ILeadService
    {
        Task<GetLeadDTO> GetByIdAsync(Guid id);

        Task<GetLeadDTO> AddAsync(AddLeadDTO dto);

        Task<GetLeadDTO> UpdateAsync(UpdateLeadDTO dto, Guid id);

        Task DeleteAsync(Guid id);

        Task<PaginationResponseDTO<GetLeadDTO>> GetAllByCustomerIdAsync(Guid customerId, LeadQueryDTO query);

        Task<PaginationResponseDTO<GetLeadDTO>> GetAllAsync(LeadQueryDTO query);

        Task<GetLeadDTO> DisqualifyLeadAsync(Guid id, DisqualifyLeadDTO model);

        Task<GetDealDTO> QualifyLeadAsync(Guid id);
    }
}