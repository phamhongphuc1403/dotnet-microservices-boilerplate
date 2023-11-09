using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs.DealDTO;

public class DealDto : Entity
{
    public string Title { get; set; } = null!;

    public Guid LeadId { get; set; }

    public string? Description { get; set; }

    public DealStatus Status { get; set; }
}