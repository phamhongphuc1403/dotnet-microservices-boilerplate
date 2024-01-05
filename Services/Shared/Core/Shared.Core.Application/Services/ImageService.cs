using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared.Core.Application.Services;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;

    public ImageService(ILogger<ImageService> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<string>> SaveAsync(IEnumerable<IFormFile> images)
    {
        var filePaths = new List<string>();

        var saveFileTasks = new List<Task>();

        foreach (var image in images)
        {
            var filePath = CreateImagePath(image);

            _logger.LogInformation($"Saving image to {filePath}");

            var stream = new FileStream(filePath, FileMode.Create);

            saveFileTasks.Add(image.CopyToAsync(stream));

            filePaths.Add(filePath);

            _logger.LogInformation($"Image saved to {filePath}");
        }

        await Task.WhenAll(saveFileTasks);

        return filePaths;
    }

    private static string CreateImagePath(IFormFile image)
    {
        var fileName =
            $"{Path.GetFileNameWithoutExtension(image.FileName)}-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(image.FileName)}";

        return Path.Combine(Directory.GetCurrentDirectory(), fileName);
    }
}