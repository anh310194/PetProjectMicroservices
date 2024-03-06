#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
#EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/MasterData/MasterData.API/MasterData.API.csproj", "Services/MasterData/MasterData.API/"]
COPY ["Services/MasterData/MasterData.Application/MasterData.Application.csproj", "Services/MasterData/MasterData.Application/"]
COPY ["Services/MasterData/MasterData.Domain/MasterData.Domain.csproj", "Services/MasterData/MasterData.Domain/"]
COPY ["Services/MasterData/MasterData.Infrastructure/MasterData.Infrastructure.csproj", "Services/MasterData/MasterData.Infrastructure/"]
RUN dotnet restore "./Services/MasterData/MasterData.API/./MasterData.API.csproj"
COPY . .
WORKDIR "/src/Services/MasterData/MasterData.API"
RUN dotnet build "./MasterData.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MasterData.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterData.API.dll"]