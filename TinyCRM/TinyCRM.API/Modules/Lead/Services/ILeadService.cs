using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Deal.DTOs;
using TinyCRM.API.Modules.Lead.DTOs;

namespace TinyCRM.API.Modules.Lead.Services
{
    public interface ILeadService
    {
        Task<GetLeadDTO> GetByIdAsync(Guid id);
        Task<GetLeadDTO> AddAsync(AddLeadDTO dto);
        Task<GetLeadDTO> UpdateAsync(UpdateLeadDTO dto, Guid id);
        Task DeleteAsync(Guid id);
        Task<IList<GetLeadDTO>> GetAllByCustomerIdAsync(Guid customerId, int? skip, int? take, string? title, string? sortBy, bool? descending);
        Task<IList<GetLeadDTO>> GetAllAsync(int? skip, int? take, string? title, string? sortBy, bool? descending);
        Task<GetLeadDTO> DisqualifyLeadAsync(Guid id, DisqualifyLeadDTO model);
        Task<GetDealDTO> QualifyLeadAsync(Guid id);
    }
}