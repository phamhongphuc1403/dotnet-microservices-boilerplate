FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Sale/Presentation/TinyCRM.SaleManagement.API/TinyCRM.SaleManagement.API.csproj", "Services/Sale/Presentation/TinyCRM.SaleManagement.API/"]
COPY ["BuildingBlocks/TinyCRM.Core/TinyCRM.Core.csproj", "BuildingBlocks/TinyCRM.Core/"]
COPY ["Services/Sale/Core/TinyCRM.SaleManagement.Domain/TinyCRM.SaleManagement.Domain.csproj", "Services/Sale/Core/TinyCRM.SaleManagement.Domain/"]
COPY ["BuildingBlocks/TinyCRM.EntityFrameworkCore/TinyCRM.EntityFrameworkCore.csproj", "BuildingBlocks/TinyCRM.EntityFrameworkCore/"]
COPY ["Services/Sale/Core/TinyCRM.SaleManagement.Application/TinyCRM.SaleManagement.Application.csproj", "Services/Sale/Core/TinyCRM.SaleManagement.Application/"]
COPY ["Services/Sale/Infrastructure/TinyCRM.SaleManagement.EntityFrameworkCore/TinyCRM.SaleManagement.EntityFrameworkCore.csproj", "Services/Sale/Infrastructure/TinyCRM.SaleManagement.EntityFrameworkCore/"]
RUN dotnet restore "Services/Sale/Presentation/TinyCRM.SaleManagement.API/TinyCRM.SaleManagement.API.csproj"
COPY . .
WORKDIR "/src/Services/Sale/Presentation/TinyCRM.SaleManagement.API"
RUN dotnet build "TinyCRM.SaleManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.SaleManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.SaleManagement.API.dll"]
