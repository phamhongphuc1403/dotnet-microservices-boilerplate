using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Deal.DTOs;

public class GetAllDealsDto : GuidBaseEntity
{
    public string Title { get; set; } = null!;
    public string Customer { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(DealStatuses))]
    public DealStatuses Status { get; set; }
}