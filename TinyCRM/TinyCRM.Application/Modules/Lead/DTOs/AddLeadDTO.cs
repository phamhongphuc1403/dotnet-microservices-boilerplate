using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Lead.DTOs;

public class AddLeadDto
{
    public string Title { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSources))]
    public LeadSources? Source { get; set; }
}