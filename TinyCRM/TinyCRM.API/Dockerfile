#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TinyCRM.API/TinyCRM.API.csproj", "TinyCRM.API/"]
COPY ["TinyCRM.Domain/TinyCRM.Domain.csproj", "TinyCRM.Domain/"]
COPY ["TinyCRM.Infrastructure/TinyCRM.Infrastructure.csproj", "TinyCRM.Infrastructure/"]
RUN dotnet restore "TinyCRM.API/TinyCRM.API.csproj"
COPY . .
WORKDIR "/src/TinyCRM.API"
RUN dotnet build "TinyCRM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.API.dll"]