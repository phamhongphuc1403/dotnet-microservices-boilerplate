using System.ComponentModel.DataAnnotations;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class UpdateDealDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}