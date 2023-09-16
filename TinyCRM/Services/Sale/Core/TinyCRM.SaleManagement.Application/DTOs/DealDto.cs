using BuildingBlock.Core;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.Application.DTOs;

public class DealDto : GuidBaseEntity
{
    public string Title { get; set; } = null!;
    public Guid LeadId { get; set; }
    public string? Description { get; set; }
    public DealStatuses Status { get; set; }
}