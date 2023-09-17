using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlock.Application.CQRS;
using TinyCRM.Sales.Application.DTOs;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Application.Commands.Requests;

public class CreateLeadCommand : ICommand<LeadDto>
{
    public string Title { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSources))]
    public LeadSources? Source { get; set; }
}