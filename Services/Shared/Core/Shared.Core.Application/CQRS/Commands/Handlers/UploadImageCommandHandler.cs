using BuildingBlock.Core.Application.CQRS;
using Shared.Core.Application.CQRS.Commands.Requests;
using Shared.Core.Application.DTOs;
using Shared.Core.Application.Services;

namespace Shared.Core.Application.CQRS.Commands.Handlers;

public class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, UploadImagesResponseDto>
{
    private readonly ICloudStorageService _cloudStorageService;
    private readonly IImageService _imageService;

    public UploadImageCommandHandler(ICloudStorageService cloudStorageService, IImageService imageService)
    {
        _cloudStorageService = cloudStorageService;
        _imageService = imageService;
    }

    public async Task<UploadImagesResponseDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var images = await _imageService.SaveAsync(request.Images);

        var uris = await _cloudStorageService.UploadAsync(images);

        return new UploadImagesResponseDto
        {
            Urls = uris
        };
    }
}