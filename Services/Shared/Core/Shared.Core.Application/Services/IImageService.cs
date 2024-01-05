using Microsoft.AspNetCore.Http;

namespace Shared.Core.Application.Services;

public interface IImageService
{
    Task<IEnumerable<string>> SaveAsync(IEnumerable<IFormFile> images);
}