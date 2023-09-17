FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Sales/Presentation/TinyCRM.Sales.API/TinyCRM.Sales.API.csproj", "Services/Sales/Presentation/TinyCRM.Sales.API/"]
COPY ["BuildingBlocks/BuildingBlock.Common/BuildingBlock.Common.csproj", "BuildingBlocks/BuildingBlock.Common/"]
COPY ["BuildingBlocks/BuildingBlock.EntityFrameworkCore/BuildingBlock.EntityFrameworkCore.csproj", "BuildingBlocks/BuildingBlock.EntityFrameworkCore/"]
COPY ["BuildingBlocks/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "BuildingBlocks/BuildingBlock.Domain/"]
COPY ["BuildingBlocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "BuildingBlocks/BuildingBlock.Application/"]
COPY ["BuildingBlocks/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "BuildingBlocks/BuildingBlock.RabbitMQ/"]
COPY ["Services/Sales/Core/TinyCRM.Sales.Application/TinyCRM.Sales.Application.csproj", "Services/Sales/Core/TinyCRM.Sales.Application/"]
COPY ["Services/Sales/Core/TinyCRM.Sales.Domain/TinyCRM.Sales.Domain.csproj", "Services/Sales/Core/TinyCRM.Sales.Domain/"]
COPY ["Services/Sales/Infrastructure/TinyCRM.Sales.EntityFrameworkCore/TinyCRM.Sales.EntityFrameworkCore.csproj", "Services/Sales/Infrastructure/TinyCRM.Sales.EntityFrameworkCore/"]
RUN dotnet restore "Services/Sales/Presentation/TinyCRM.Sales.API/TinyCRM.Sales.API.csproj"
COPY . .
WORKDIR "/src/Services/Sales/Presentation/TinyCRM.Sales.API"
RUN dotnet build "TinyCRM.Sales.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.Sales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.Sales.API.dll"]
