FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/Identity

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /src/Identity
# COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Identity.dll"]

# WORKDIR /src/Identity
# COPY "src/Identity/Identity.csproj" .
# # Restore as distinct layers
# RUN dotnet 
# COPY "src/Identity" .
# RUN dotnet publish -c release -o /app