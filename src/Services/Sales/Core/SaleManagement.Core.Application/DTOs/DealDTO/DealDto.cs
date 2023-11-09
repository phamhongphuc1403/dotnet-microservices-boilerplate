using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.DealAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.DTOs.DealDTO;

public class DealDto : Entity
{
    public string Title { get; set; } = null!;

    public Guid LeadId { get; set; }

    public string? Description { get; set; }

    public DealStatus Status { get; set; }
}