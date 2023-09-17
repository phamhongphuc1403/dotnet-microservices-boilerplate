﻿using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs;

public class DealDto : GuidBaseEntity
{
    public string Title { get; set; } = null!;
    public Guid LeadId { get; set; }
    public string? Description { get; set; }
    public DealStatuses Status { get; set; }
}