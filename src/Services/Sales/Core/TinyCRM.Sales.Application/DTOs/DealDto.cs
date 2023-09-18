using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs;

public class DealDto : GuidEntity
{
    public string Title { get; set; } = null!;
    public Guid LeadId { get; set; }
    public string? Description { get; set; }
    public DealStatus Status { get; set; }
}