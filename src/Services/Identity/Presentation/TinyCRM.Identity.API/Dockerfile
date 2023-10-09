FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Presentation/TinyCRM.Identity.API/TinyCRM.Identity.API.csproj", "Services/Identity/Presentation/TinyCRM.Identity.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Application/BuildingBlock.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Domain/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.API/BuildingBlock.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.API/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.EntityFrameworkCore/BuildingBlock.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Identity/BuildingBlocks.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Identity/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Redis/BuildingBlock.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Redis/"]
COPY ["Services/Identity/Core/TinyCRM.Identity.Application/TinyCRM.Identity.Application.csproj", "Services/Identity/Core/TinyCRM.Identity.Application/"]
COPY ["Services/Identity/Core/TinyCRM.Identity.Domain/TinyCRM.Identity.Domain.csproj", "Services/Identity/Core/TinyCRM.Identity.Domain/"]
COPY ["Services/Identity/Infrastructure/TinyCRM.Identity.EntityFrameworkCore/TinyCRM.Identity.EntityFrameworkCore.csproj", "Services/Identity/Infrastructure/TinyCRM.Identity.EntityFrameworkCore/"]
COPY ["Services/Identity/Infrastructure/TinyCRM.Identity.Identity/TinyCRM.Identity.Identity.csproj", "Services/Identity/Infrastructure/TinyCRM.Identity.Identity/"]
RUN dotnet restore "Services/Identity/Presentation/TinyCRM.Identity.API/TinyCRM.Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Presentation/TinyCRM.Identity.API"
RUN dotnet build "TinyCRM.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.Identity.API.dll"]
