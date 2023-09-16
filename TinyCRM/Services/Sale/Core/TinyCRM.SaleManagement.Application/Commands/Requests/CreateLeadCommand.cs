using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlock.Core.CQRS;
using TinyCRM.SaleManagement.Application.DTOs;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.Application.Commands.Requests;

public class CreateLeadCommand : ICommand<LeadDto>
{
    public string Title { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSources))]
    public LeadSources? Source { get; set; }
}