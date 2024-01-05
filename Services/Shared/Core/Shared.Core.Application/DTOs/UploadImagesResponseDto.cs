namespace Shared.Core.Application.DTOs;

public class UploadImagesResponseDto
{
    public IEnumerable<string> Urls { get; set; } = null!;
}