FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Notification/Presentation/NotificationManagement.Presentation.API/NotificationManagement.Presentation.API.csproj", "Services/Notification/Presentation/NotificationManagement.Presentation.API/"]
COPY ["BuildingBlocks/Presentation/BuildingBlock.Presentation.API/BuildingBlock.Presentation.API.csproj", "BuildingBlocks/Presentation/BuildingBlock.Presentation.API/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Application/BuildingBlock.Core.Application.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlock.Core.Domain/BuildingBlock.Core.Domain.csproj", "BuildingBlocks/Core/BuildingBlock.Core.Domain/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/BuildingBlock.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/BuildingBlock.Infrastructure.EntityFrameworkCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/BuildingBlock.Infrastructure.RabbitMQ.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.RabbitMQ/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/BuildingBlock.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/BuildingBlock.Infrastructure.Identity.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Identity/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/BuildingBlock.Infrastructure.Mailkit.csproj", "BuildingBlocks/Infrastructure/BuildingBlock.Infrastructure.Mailkit/"]
COPY ["Services/Notification/Infrastructure/NotificationManagement.Infrastructure.EntityFrameworkCore/NotificationManagement.Infrastructure.EntityFrameworkCore.csproj", "Services/Notification/Infrastructure/NotificationManagement.Infrastructure.EntityFrameworkCore/"]
COPY ["Services/Notification/Core/NotificationManagement.Core.Application/NotificationManagement.Core.Application.csproj", "Services/Notification/Core/NotificationManagement.Core.Application/"]
COPY ["Services/Notification/Core/NotificationManagement.Core.Domain/NotificationManagement.Core.Domain.csproj", "Services/Notification/Core/NotificationManagement.Core.Domain/"]
COPY ["Services/Notification/Infrastructure/NotificationManagement.Infrastructure.Firebase/NotificationManagement.Infrastructure.Firebase.csproj", "Services/Notification/Infrastructure/NotificationManagement.Infrastructure.Firebase/"]
RUN dotnet restore "Services/Notification/Presentation/NotificationManagement.Presentation.API/NotificationManagement.Presentation.API.csproj"
COPY . .
WORKDIR "/src/Services/Notification/Presentation/NotificationManagement.Presentation.API"
RUN dotnet build "NotificationManagement.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationManagement.Presentation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationManagement.Presentation.API.dll"]
