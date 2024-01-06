using System.Text.Json.Serialization;
using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Utilities;
using CloudinaryDotNet;
using FluentValidation;
using Shared.Core.Application;
using Shared.Core.Application.Services;
using Shared.Infrastructure.Cloudinary;

namespace Shared.Presentation.API.Extensions;

public static class SharedExtensions
{
    public static IServiceCollection AddSharedExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddGrpcAuthentication(configuration)
            .AddGrpcAuthorization();

        services
            .RegisterServices<SharedApplicationAssemblyReference>();

        services.AddScoped<ICloudStorageService, CloudinaryCloudStorageService>(sp =>
        {
            var cloudinaryConnectionString = configuration.GetRequiredValue("CloudStorage:ConnectionString");
            var saveLocation = configuration.GetRequiredValue("CloudStorage:Location");

            var fileSystemService = sp.GetRequiredService<IFileSystemService>();

            var cloudinary = new Cloudinary(cloudinaryConnectionString);

            var logger = sp.GetRequiredService<ILogger<CloudinaryCloudStorageService>>();

            return new CloudinaryCloudStorageService(cloudinary, fileSystemService, saveLocation, logger);
        });

        services
            .AddApplicationCors(configuration)
            .AddHttpContextAccessor()
            .AddCurrentUser()
            .AddCqrs<SharedApplicationAssemblyReference, SharedApplicationAssemblyReference>()
            .AddDefaultOpenApi(configuration)
            .AddValidatorsFromAssembly(typeof(SharedApplicationAssemblyReference).Assembly)
            .AddInMemoryCache(configuration);

        return services;
    }
}