using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Lead.DTOs;

public class GetLeadDto : GuidBaseEntity
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