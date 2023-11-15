FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Products/Presentation/ProductManagement.Presentation.API/ProductManagement.Presentation.API.csproj", "Services/Products/Presentation/ProductManagement.Presentation.API/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.Presentation.API/BuildingBlock.Presentation.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Application/BuildingBlock.Core.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Domain/BuildingBlock.Core.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Domain/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/BuildingBlock.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/BuildingBlock.Infrastructure.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/BuildingBlock.Infrastructure.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/BuildingBlock.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/BuildingBlock.Infrastructure.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/"]
COPY ["Services/Products/Core/ProductManagement.Core.Application/ProductManagement.Core.Application.csproj", "Services/Products/Core/ProductManagement.Core.Application/"]
COPY ["Services/Products/Core/ProductManagement.Core.Domain/ProductManagement.Core.Domain.csproj", "Services/Products/Core/ProductManagement.Core.Domain/"]
COPY ["Services/Products/Infrastructure/ProductManagement.Infrastructure.EntityFrameworkCore/ProductManagement.Infrastructure.EntityFrameworkCore.csproj", "Services/Products/Infrastructure/ProductManagement.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "Services/Products/Presentation/ProductManagement.Presentation.API/ProductManagement.Presentation.API.csproj"
COPY . .
WORKDIR "/src/Services/Products/Presentation/ProductManagement.Presentation.API"
RUN dotnet build "ProductManagement.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductManagement.Presentation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.Presentation.API.dll"]
