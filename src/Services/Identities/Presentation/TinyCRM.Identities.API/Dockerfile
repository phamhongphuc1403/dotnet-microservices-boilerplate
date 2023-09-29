FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identities/Presentation/TinyCRM.Identities.API/TinyCRM.Identities.API.csproj", "Services/Identities/Presentation/TinyCRM.Identities.API/"]
COPY ["BuildingBlocks/BuildingBlock.Common/BuildingBlock.Common.csproj", "BuildingBlocks/BuildingBlock.Common/"]
COPY ["BuildingBlocks/BuildingBlock.EntityFrameworkCore/BuildingBlock.EntityFrameworkCore.csproj", "BuildingBlocks/BuildingBlock.EntityFrameworkCore/"]
COPY ["BuildingBlocks/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "BuildingBlocks/BuildingBlock.Domain/"]
COPY ["BuildingBlocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "BuildingBlocks/BuildingBlock.Application/"]
COPY ["BuildingBlocks/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "BuildingBlocks/BuildingBlock.RabbitMQ/"]
COPY ["Services/Identities/Core/TinyCRM.Identities.Application/TinyCRM.Identities.Application.csproj", "Services/Identities/Core/TinyCRM.Identities.Application/"]
COPY ["Services/Identities/Core/TinyCRM.Identities.Domain/TinyCRM.Identities.Domain.csproj", "Services/Identities/Core/TinyCRM.Identities.Domain/"]
COPY ["Services/Identities/Infrastructure/TinyCRM.Identities.EntityFrameworkCore/TinyCRM.Identities.EntityFrameworkCore.csproj", "Services/Identities/Infrastructure/TinyCRM.Identities.EntityFrameworkCore/"]
RUN dotnet restore "Services/Identities/Presentation/TinyCRM.Identities.API/TinyCRM.Identities.API.csproj"
COPY . .
WORKDIR "/src/Services/Identities/Presentation/TinyCRM.Identities.API"
RUN dotnet build "TinyCRM.Identities.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.Identities.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.Identities.API.dll"]
