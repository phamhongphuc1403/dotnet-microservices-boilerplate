using BuildingBlock.Core.Application.CQRS;
using Microsoft.AspNetCore.Http;
using Shared.Core.Application.DTOs;

namespace Shared.Core.Application.CQRS.Commands.Requests;

public record UploadImageCommand(IEnumerable<IFormFile> Images) : ICommand<UploadImagesResponseDto>;