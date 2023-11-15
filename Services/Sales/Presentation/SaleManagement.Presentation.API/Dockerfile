FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Sales/Presentation/SaleManagement.Presentation.API/SaleManagement.Presentation.API.csproj", "Services/Sales/Presentation/SaleManagement.Presentation.API/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.Presentation.API/BuildingBlock.Presentation.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Application/BuildingBlock.Core.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Domain/BuildingBlock.Core.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Domain/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/BuildingBlock.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/BuildingBlock.Infrastructure.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/BuildingBlock.Infrastructure.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/BuildingBlock.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/BuildingBlock.Infrastructure.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/"]
COPY ["Services/Sales/Core/SaleManagement.Core.Application/SaleManagement.Core.Application.csproj", "Services/Sales/Core/SaleManagement.Core.Application/"]
COPY ["Services/Sales/Core/SaleManagement.Core.Domain/SaleManagement.Core.Domain.csproj", "Services/Sales/Core/SaleManagement.Core.Domain/"]
COPY ["Services/Sales/Infrastructure/SaleManagement.Infrastructure.EntityFrameworkCore/SaleManagement.Infrastructure.EntityFrameworkCore.csproj", "Services/Sales/Infrastructure/SaleManagement.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "Services/Sales/Presentation/SaleManagement.Presentation.API/SaleManagement.Presentation.API.csproj"
COPY . .
WORKDIR "/src/Services/Sales/Presentation/SaleManagement.Presentation.API"
RUN dotnet build "SaleManagement.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SaleManagement.Presentation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SaleManagement.Presentation.API.dll"]
