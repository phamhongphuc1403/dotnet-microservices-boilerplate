namespace Shared.Core.Application.Services;

public interface ICloudStorageService
{
    Task<string> UploadAsync(string imagePath);

    Task<IEnumerable<string>> UploadAsync(IEnumerable<string> imagePaths);
}