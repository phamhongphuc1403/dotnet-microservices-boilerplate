FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Shared/Presentation/Shared.Presentation.API/Shared.Presentation.API.csproj", "Services/Shared/Presentation/Shared.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Domain/BuildingBlock.Core.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Domain/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.Presentation.API/BuildingBlock.Presentation.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Application/BuildingBlock.Core.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Application/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/BuildingBlock.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/BuildingBlock.Infrastructure.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/BuildingBlock.Infrastructure.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/BuildingBlock.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/BuildingBlock.Infrastructure.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/BuildingBlock.Infrastructure.Mailkit.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/"]
COPY ["Services/Shared/Core/Shared.Core.Application/Shared.Core.Application.csproj", "Services/Shared/Core/Shared.Core.Application/"]
COPY ["Services/Shared/Infrastructure/Shared.Infrastructure.Cloudinary/Shared.Infrastructure.Cloudinary.csproj", "Services/Shared/Infrastructure/Shared.Infrastructure.Cloudinary/"]
RUN dotnet restore "Services/Shared/Presentation/Shared.Presentation.API/Shared.Presentation.API.csproj"
COPY . .
WORKDIR "/src/Services/Shared/Presentation/Shared.Presentation.API"
RUN dotnet build "Shared.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shared.Presentation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shared.Presentation.API.dll"]
