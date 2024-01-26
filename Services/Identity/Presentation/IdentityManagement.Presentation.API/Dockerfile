FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Presentation/IdentityManagement.Presentation.API/IdentityManagement.Presentation.API.csproj", "Services/Identity/Presentation/IdentityManagement.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Application/BuildingBlock.Core.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Domain/BuildingBlock.Core.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Domain/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.Presentation.API/BuildingBlock.Presentation.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.Presentation.API/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/BuildingBlock.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/BuildingBlock.Infrastructure.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/BuildingBlock.Infrastructure.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/BuildingBlock.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/BuildingBlock.Infrastructure.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/BuildingBlock.Infrastructure.Mailkit.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/"]
COPY ["Services/Identity/Core/IdentityManagement.Core.Application/IdentityManagement.Core.Application.csproj", "Services/Identity/Core/IdentityManagement.Core.Application/"]
COPY ["Services/Identity/Core/IdentityManagement.Core.Domain/IdentityManagement.Core.Domain.csproj", "Services/Identity/Core/IdentityManagement.Core.Domain/"]
COPY ["Services/Identity/Infrastructure/IdentityManagement.Infrastructure.EntityFrameworkCore/IdentityManagement.Infrastructure.EntityFrameworkCore.csproj", "Services/Identity/Infrastructure/IdentityManagement.Infrastructure.EntityFrameworkCore/"]
COPY ["Services/Identity/Infrastructure/IdentityManagement.Infrastructure.Identity/IdentityManagement.Infrastructure.Identity.csproj", "Services/Identity/Infrastructure/IdentityManagement.Infrastructure.Identity/"]
RUN dotnet restore "Services/Identity/Presentation/IdentityManagement.Presentation.API/IdentityManagement.Presentation.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Presentation/IdentityManagement.Presentation.API"
RUN dotnet build "IdentityManagement.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityManagement.Presentation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityManagement.Presentation.API.dll"]
