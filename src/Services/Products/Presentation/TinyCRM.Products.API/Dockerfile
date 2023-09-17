FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Products/Presentation/TinyCRM.Products.API/TinyCRM.Products.API.csproj", "Services/Products/Presentation/TinyCRM.Products.API/"]
COPY ["BuildingBlocks/BuildingBlock.Common/BuildingBlock.Common.csproj", "BuildingBlocks/BuildingBlock.Common/"]
COPY ["BuildingBlocks/BuildingBlock.EntityFrameworkCore/BuildingBlock.EntityFrameworkCore.csproj", "BuildingBlocks/BuildingBlock.EntityFrameworkCore/"]
COPY ["BuildingBlocks/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "BuildingBlocks/BuildingBlock.Domain/"]
COPY ["BuildingBlocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "BuildingBlocks/BuildingBlock.Application/"]
COPY ["BuildingBlocks/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "BuildingBlocks/BuildingBlock.RabbitMQ/"]
COPY ["Services/Products/Core/TinyCRM.Products.Application/TinyCRM.Products.Application.csproj", "Services/Products/Core/TinyCRM.Products.Application/"]
COPY ["Services/Products/Core/TinyCRM.Products.Domain/TinyCRM.Products.Domain.csproj", "Services/Products/Core/TinyCRM.Products.Domain/"]
COPY ["Services/Products/Infrastructure/TinyCRM.Products.EntityFrameworkCore/TinyCRM.Products.EntityFrameworkCore.csproj", "Services/Products/Infrastructure/TinyCRM.Products.EntityFrameworkCore/"]
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
