FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Product/Presentation/TinyCRM.ProductManagement.API/TinyCRM.ProductManagement.API.csproj", "Services/Product/Presentation/TinyCRM.ProductManagement.API/"]
COPY ["BuildingBlocks/TinyCRM.Core/TinyCRM.Core.csproj", "BuildingBlocks/TinyCRM.Core/"]
COPY ["Services/Product/Core/TinyCRM.ProductManagement.Domain/TinyCRM.ProductManagement.Domain.csproj", "Services/Product/Core/TinyCRM.ProductManagement.Domain/"]
COPY ["BuildingBlocks/TinyCRM.EntityFrameworkCore/TinyCRM.EntityFrameworkCore.csproj", "BuildingBlocks/TinyCRM.EntityFrameworkCore/"]
COPY ["Services/Product/Infrastructure/TinyCRM.ProductManagement.EntityFrameworkCore/TinyCRM.ProductManagement.EntityFrameworkCore.csproj", "Services/Product/Infrastructure/TinyCRM.ProductManagement.EntityFrameworkCore/"]
COPY ["Services/Product/Core/TinyCRM.ProductManagement.Application/TinyCRM.ProductManagement.Application.csproj", "Services/Product/Core/TinyCRM.ProductManagement.Application/"]
RUN dotnet restore "Services/Product/Presentation/TinyCRM.ProductManagement.API/TinyCRM.ProductManagement.API.csproj"
COPY . .
WORKDIR "/src/Services/Product/Presentation/TinyCRM.ProductManagement.API"
RUN dotnet build "TinyCRM.ProductManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.ProductManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.ProductManagement.API.dll"]
