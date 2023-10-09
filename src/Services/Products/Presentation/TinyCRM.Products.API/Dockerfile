FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Products/Presentation/TinyCRM.Products.API/TinyCRM.Products.API.csproj", "Services/Products/Presentation/TinyCRM.Products.API/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Identity/BuildingBlocks.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Identity/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.RabbitMQ/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Application/BuildingBlock.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Domain/"]
COPY ["Services/Products/Core/TinyCRM.Products.Application/TinyCRM.Products.Application.csproj", "Services/Products/Core/TinyCRM.Products.Application/"]
COPY ["Services/Products/Core/TinyCRM.Products.Domain/TinyCRM.Products.Domain.csproj", "Services/Products/Core/TinyCRM.Products.Domain/"]
COPY ["Services/Products/Infrastructure/TinyCRM.Products.EntityFrameworkCore/TinyCRM.Products.EntityFrameworkCore.csproj", "Services/Products/Infrastructure/TinyCRM.Products.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.EntityFrameworkCore/BuildingBlock.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.API/BuildingBlock.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.API/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Redis/BuildingBlock.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Redis/"]
RUN dotnet restore "Services/Products/Presentation/TinyCRM.Products.API/TinyCRM.Products.API.csproj"
COPY . .
WORKDIR "/src/Services/Products/Presentation/TinyCRM.Products.API"
RUN dotnet build "TinyCRM.Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.Products.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.Products.API.dll"]
