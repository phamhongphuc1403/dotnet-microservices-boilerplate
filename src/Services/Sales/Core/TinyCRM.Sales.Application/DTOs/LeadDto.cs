using System.Text.Json.Serialization;
using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.DTOs;

public class LeadDto : GuidBaseEntity
{
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadSources? Source { get; set; }

    public double? EstimatedRevenue { get; set; }
    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadStatuses? Status { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadDisqualificationReasons? DisqualificationReason { get; set; }

    public string? DisqualificationDescription { get; set; }
}