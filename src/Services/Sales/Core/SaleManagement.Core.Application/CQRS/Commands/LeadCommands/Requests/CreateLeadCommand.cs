using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlock.Core.Application.CQRS;
using SaleManagement.Core.Application.DTOs.LeadDTOs;
using SaleManagement.Core.Domain.LeadAggregate.Entities.Enums;

namespace SaleManagement.Core.Application.CQRS.Commands.LeadCommands.Requests;

public class CreateLeadCommand : ICommand<LeadDto>
{
    public string Title { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSource))]
    public LeadSource? Source { get; set; }
}